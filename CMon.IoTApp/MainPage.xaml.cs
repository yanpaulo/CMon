﻿using CMon.IoTApp.Models;
using CMon.IoTApp.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Enumeration;
using Windows.Devices.SerialCommunication;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CMon.IoTApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private MainPageViewModel _viewModel;
        private DateTime _startDate;

        private SerialDevice _device;
        private DispatcherTimer _timer;
        private DataWriter _dataWriter;
        private DataReader _dataReader;
        private Random rng = new Random();

        public MainPage()
        {
            this.InitializeComponent();
            _viewModel = (MainPageViewModel)DataContext;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //string aqs = SerialDevice.GetDeviceSelector();
            //var dis = await DeviceInformation.FindAllAsync(aqs);
            //var info = dis.First();

            //_device = await SerialDevice.FromIdAsync(info.Id);
            //_device.BaudRate = 9600;
            //_device.ReadTimeout = TimeSpan.FromMilliseconds(200);
            //_dataReader = new DataReader(_device.InputStream);
            //_dataWriter = new DataWriter(_device.OutputStream);

            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _timer.Tick += Timer_Tick;

            _startDate = DateTime.Now;
            _timer.Start();
        }

        private async void Timer_Tick(object sender, object e)
        {
            //_dataWriter.WriteString("0");
            //await _dataWriter.StoreAsync().AsTask();

            //var bytes = await _dataReader.LoadAsync(1024).AsTask();
            //var json = _dataReader.ReadString(bytes);
            //Debug.Write(json);
            //var reading = JsonConvert.DeserializeObject<Reading>(json);

            //_viewModel.Power = _viewModel.Voltage * reading.Current;
            //_viewModel.ConsumptionKW += _viewModel.Power / (3600 * 1000);
            _viewModel.Power = rng.Next(10, 15);
            _viewModel.Time = DateTime.Now - _startDate;
            _viewModel.ChartMinimumTime = _viewModel.Time - TimeSpan.FromSeconds(60);


            _viewModel.ChartItems.Add(new MainChartViewModelItem { Power = _viewModel.Power, Time = _viewModel.Time });
            if (_viewModel.ChartItems.First().Time < _viewModel.ChartMinimumTime)
            {
                _viewModel.ChartItems.RemoveAt(0);
                _viewModel.ChartMinimumTime = _viewModel.ChartItems.First().Time;
            }
            else
            {
                
            }
        }
    }
}
