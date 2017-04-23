using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMon.IoTApp.Models
{
    public class AppConfiguration
    {
        public int Id { get; set; }

        public int Voltage { get; set; } = 220;

        public decimal Tax { get; set; } = 0.5m;
    }
}
