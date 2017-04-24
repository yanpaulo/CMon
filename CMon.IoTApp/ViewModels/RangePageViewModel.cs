using CMon.IoTApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMon.IoTApp.ViewModels
{
    public class RangePageViewModel : INotifyPropertyChanged
    {
        private DateTime _chartStartDate;
        private DateTime _chartEndDate;
        private IEnumerable<Reading> _readings;

        public RangePageViewModel()
        {
            _chartStartDate = DateTime.Today - TimeSpan.FromDays(30);
            _chartEndDate = DateTime.Today;
            StartDate = new DateTimeOffset(_chartStartDate);
            EndDate = new DateTimeOffset(_chartEndDate);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset EndDate { get; set; }

        public DateTime ChartStartDate
        {
            get { return _chartStartDate; }
            set { _chartStartDate = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ChartStartDate")); }
        }

        public DateTime ChartEndDate
        {
            get { return _chartEndDate; }
            set { _chartEndDate = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ChartEndDate")); }
        }
        
        public IEnumerable<Reading> Readings
        {
            get { return _readings; }
            set { _readings = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Readings")); }
        }


    }
}
