using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMon.IoTApp.ViewModels
{
    public class MainPageViewModel
    {
        private double _power;
        private double _consumptionKW;
        private TimeSpan _time;
        public int Voltage { get; set; } = 220;

        public decimal Tax { get; set; } = 0.5m;

        public double Power
        {
            get { return _power; }
            set { _power = value; }
        }
        public TimeSpan Time
        {
            get { return _time; }
            set { _time = value; }
        }
        
        public double ConsumptionKW
        {
            get { return _consumptionKW; }
            set { _consumptionKW = value; }
        }
        
        public decimal ConsumptionMoney => (decimal)ConsumptionKW * Tax;

    }
}
