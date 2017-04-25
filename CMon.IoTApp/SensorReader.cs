using CMon.IoTApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.SerialCommunication;
using Windows.Storage.Streams;
using Windows.System.Threading;

namespace CMon.IoTApp
{
    public class SensorReader
    {
        public static SensorReader Instance { get; } = new SensorReader();
        private SensorReader() { }

        private ThreadPoolTimer _timer;
        private bool _started;
        private object _lockObject = new object();

        private SerialDevice _device;
        private DataReader _dataReader;
        private DataWriter _dataWriter;

        public void Start()
        {
            lock (_lockObject)
            {
                if (!_started)
                {
                    Task.Run(StartAsync).Wait();
                }
            }
        }

        private async Task StartAsync()
        {
            string aqs = SerialDevice.GetDeviceSelector();
            var dis = await DeviceInformation.FindAllAsync(aqs);
            var info = dis.First();

            _device = await SerialDevice.FromIdAsync(info.Id);
            _device.BaudRate = 9600;
            _device.ReadTimeout = TimeSpan.FromMilliseconds(200);
            _dataReader = new DataReader(_device.InputStream);
            _dataWriter = new DataWriter(_device.OutputStream);

            _timer = ThreadPoolTimer.CreatePeriodicTimer(Timer_Tick, TimeSpan.FromSeconds(1));
            _started = true;
        }

        private async void Timer_Tick(ThreadPoolTimer t)
        {
            _dataWriter.WriteString("0");
            await _dataWriter.StoreAsync();

            var bytes = await _dataReader.LoadAsync(1024);
            var json = _dataReader.ReadString(bytes);
            Debug.Write(json);
            var reading = JsonConvert.DeserializeObject<RawReading>(json);

            if (reading.Current >= 0.08)
            {
                using (var db = new AppDbContext())
                {
                    var config = db.GetAppConfiguration();
                    db.Readings.Add(new Models.Reading
                    {
                        Value = reading.Current,
                        Voltage = config.Voltage,
                        Power = config.Voltage * reading.Current,
                        Date = DateTime.Now
                    });
                    lock (AppDbContext.LockObject)
                    {
                        db.SaveChanges(); 
                    }
                }
            }
        }
    }
}
