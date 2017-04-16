using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMon.IoTApp.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private double _power;
        private double _consumptionKW;
        private TimeSpan _time;

        public event PropertyChangedEventHandler PropertyChanged;

        public int Voltage { get; set; } = 220;

        public decimal Tax { get; set; } = 0.5m;

        public double Power
        {
            get { return _power; }
            set { _power = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Power")); }
        }
        public TimeSpan Time
        {
            get { return _time; }
            set { _time = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Time")); }
        }

        public double ConsumptionKW
        {
            get { return _consumptionKW; }
            set
            {
                _consumptionKW = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ConsumptionKW"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ConsumptionMoney"));
            }
        }

        public decimal ConsumptionMoney => (decimal)ConsumptionKW * Tax;

    }
}
