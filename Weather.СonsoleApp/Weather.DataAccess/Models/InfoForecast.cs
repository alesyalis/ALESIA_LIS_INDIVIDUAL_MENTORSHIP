using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Weather.DataAccess.Models
{
    public class InfoForecast
    {
        public ForecastDescription Main { get; set; }

        [JsonProperty("dt_txt")]
        public DateTime Date { get; set; }
    }


}

