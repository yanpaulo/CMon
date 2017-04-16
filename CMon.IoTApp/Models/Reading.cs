using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMon.IoTApp.Models
{
    [JsonObject(NamingStrategyType=typeof(SnakeCaseNamingStrategy))]
    public class Reading
    {
        public double Current { get; set; }

        public int Readings { get; set; }

        public int SquareSum { get; set; }
    }
}
