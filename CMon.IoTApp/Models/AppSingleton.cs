using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMon.IoTApp.Models
{
    public class AppSingleton
    {
        public static AppSingleton Instance { get; } = new AppSingleton();

        public List<Reading> Readings { get; set; } = new List<Reading>();
    }
}
