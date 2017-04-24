using CMon.IoTApp.Models;
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
    public sealed partial class RealTimePage : Page
    {
        private DateTime _startDate;
        private DispatcherTimer _timer;
        private RealTimeViewModel _viewModel;
        private List<Reading> _readings;

        public RealTimePage()
        {
            this.InitializeComponent();
            _viewModel = (RealTimeViewModel)DataContext;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            using (var db =  new AppDbContext())
            {
                var config = db.GetAppConfiguration();
                _viewModel.Voltage = config.Voltage;
                _viewModel.Tax = config.Tax;
            }

            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _timer.Tick += Timer_Tick;

            _startDate = DateTime.Now;
            _timer.Start();
        }

        private void Timer_Tick(object sender, object e)
        {
            var now = DateTime.Now;
            
            using (var db = new AppDbContext())
            {
                DateTime min = now - TimeSpan.FromSeconds(60);
                _readings = db.Readings
                    .Where(r => r.Date > min)
                    .OrderByDescending(r => r.Date)
                    .ToList();

                var last = _readings.FirstOrDefault();
                if (last != null)
                {
                    last.Date = now;
                }
            }


            _viewModel.Power = _viewModel.Voltage * _readings.FirstOrDefault()?.Value;
            _viewModel.ConsumptionKW += _viewModel.Power / (3600 * 1000);
            _viewModel.Time = now - _startDate;
            
            UpdateChart(now);
        }

        private void UpdateChart(DateTime now)
        {
            _viewModel.ChartItems =
                _readings
                .Select(r => new RealTimeViewModelItem { Power = r.Value * _viewModel.Voltage.GetValueOrDefault(0), Time = now - r.Date })
                .ToArray();
        }
    }
}
