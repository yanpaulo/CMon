using CMon.IoTApp.Models;
using CMon.IoTApp.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CMon.IoTApp.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RangePage : Page
    {
        private RangePageViewModel _viewModel;

        public RangePage()
        {
            this.InitializeComponent();
            _viewModel = (RangePageViewModel)DataContext;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateChart();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateChart();
        }

        private void UpdateChart()
        {
            using (var db = new AppDbContext())
            {
                _viewModel.ChartStartDate = _viewModel.StartDate.Date;
                _viewModel.ChartEndDate = _viewModel.EndDate.Date;

                var readings = db.Readings
                    .Where(r => r.Date >= _viewModel.StartDate && r.Date <= _viewModel.EndDate)
                    .OrderBy(r => r.Date)
                    .ToList();
                _viewModel.Readings = readings
                    .GroupBy(r => r.Date.Date)
                    .Select(g => new Reading { Date = g.Key, Power = g.Sum(r => r.Power) / (3600 * 1000) })
                    .ToList();
            }
        }
    }
}
