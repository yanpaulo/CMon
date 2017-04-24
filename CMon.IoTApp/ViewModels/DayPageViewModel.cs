using CMon.IoTApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMon.IoTApp.ViewModels
{
    public class DayPageViewModel : INotifyPropertyChanged
    {
        public DateTimeOffset Date { get; set; } = new DateTimeOffset(DateTime.Today);

        private IEnumerable<Reading> _readings;

        public event PropertyChangedEventHandler PropertyChanged;

        public IEnumerable<Reading> Readings
        {
            get { return _readings; }
            set { _readings = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Readings")); }
        }

    }
}
