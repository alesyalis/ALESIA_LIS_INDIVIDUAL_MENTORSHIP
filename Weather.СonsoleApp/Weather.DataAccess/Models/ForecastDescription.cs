using Newtonsoft.Json;
using System;

namespace Weather.DataAccess.Models
{
    public class ForecastDescription : TemperatureInfo
    {
        [JsonProperty("temp_min")]
        public double MinTemp { get; set; }

        [JsonProperty("temp_max")]
        public double MaxTemp { get; set; }
       public DateTime DateTime { get; set; }  
    }
}
