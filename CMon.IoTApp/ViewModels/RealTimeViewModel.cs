using CMon.IoTApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMon.IoTApp.ViewModels
{
    public class RealTimeViewModel : INotifyPropertyChanged
    {
        private int? _voltage;
        private decimal? _tax;
        private double? _power;
        private double? _consumptionKW;
        private TimeSpan _time;
        private IEnumerable<RealTimeViewModelItem> _chartItems;

        public event PropertyChangedEventHandler PropertyChanged;
        
        public int? Voltage
        {
            get { return _voltage; }
            set { _voltage = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Voltage")); }
        }
        
        public decimal? Tax
        {
            get { return _tax; }
            set { _tax = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Tax")); }
        }
        
        public double? Power
        {
            get { return _power; }
            set { _power = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Power")); }
        }

        public TimeSpan Time
        {
            get { return _time; }
            set { _time = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Time")); }
        }

        public double? ConsumptionKW
        {
            get { return _consumptionKW; }
            set
            {
                _consumptionKW = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ConsumptionKW"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ConsumptionMoney"));
            }
        }

        public decimal? ConsumptionMoney => (decimal?)ConsumptionKW * Tax;

        public IEnumerable<RealTimeViewModelItem> ChartItems
        {
            get { return _chartItems; }
            set { _chartItems = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ChartItems")); }
        }
    }
}
