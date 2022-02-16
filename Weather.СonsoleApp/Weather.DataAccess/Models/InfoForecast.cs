using Newtonsoft.Json;
using System;

namespace Weather.DataAccess.Models
{
    public class InfoForecast
    {
        public ForecastDescription Main { get; set; }

        [JsonProperty("dt_txt")]
        public DateTime Date { get; set; }
    }


}

