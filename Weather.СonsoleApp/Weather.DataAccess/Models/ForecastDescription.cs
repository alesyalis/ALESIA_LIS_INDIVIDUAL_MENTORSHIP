using System;

namespace Weather.DataAccess.Models
{
    public class ForecastDescription : TemperatureInfo
    {
        public double Temp_min { get; set; }
        public double Temp_max { get; set; }
       public DateTime DateTime { get; set; }  
    }
}
