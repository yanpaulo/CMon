using CMon.IoTApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.System.Threading;

namespace CMon.IoTApp
{
    public class DataSynchronizer
    {
        public static DataSynchronizer Instance { get; private set; } = new DataSynchronizer();
        private DataSynchronizer()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:51604/api/Sync")
            };
        }

        private ThreadPoolTimer _timer;
        private bool _started;
        private object _lockObject = new object();

        private HttpClient _client;

        public void Start()
        {
            lock (_lockObject)
            {
                if (!_started)
                {
                    _timer = ThreadPoolTimer.CreatePeriodicTimer(Timer_Tick, TimeSpan.FromMinutes(1));
                    _started = true;
                }
            }
        }

        private async void Timer_Tick(ThreadPoolTimer t)
        {
            using (var db = new AppDbContext())
            {
                var readings = db.Readings
                    .Where(r => !r.Synchronized)
                    .OrderBy(r => r.Date)
                    .Take(150)
                    .ToList();

                var content = new StringContent(JsonConvert.SerializeObject(readings), Encoding.UTF8, "application/json");
                var result = await _client.PostAsync("", content);
                if (result.IsSuccessStatusCode)
                {
                    readings.ForEach(r => r.Synchronized = true);
                    lock (AppDbContext.LockObject)
                    {
                        db.SaveChanges();
                    }
                }
            }
        }
    }
}
