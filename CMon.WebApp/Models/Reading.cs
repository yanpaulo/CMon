using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMon.WebApp.Models
{
    public class Reading
    {
        public int Id { get; set; }

        public double Value { get; set; }

        public int Voltage { get; set; }

        public double Power { get; set; }
        
        public DateTime Date { get; set; }

        public string ApplicationUserId { get; set; }
    }
}