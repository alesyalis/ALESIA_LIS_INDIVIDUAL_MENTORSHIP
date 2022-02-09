using System;
using System.Collections.Generic;
using System.Text;

namespace Weather.DataAccess.Models
{
    public class ForecastDescription
    {
        public double Temp { get; set; }
        public double Temp_min { get; set; }
        public double Temp_max { get; set; }
        public string Description { get; set; }
    }
}
