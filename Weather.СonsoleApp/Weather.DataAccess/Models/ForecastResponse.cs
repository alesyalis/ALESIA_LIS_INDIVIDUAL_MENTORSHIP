using Newtonsoft.Json;
using System.Collections.Generic;

namespace Weather.DataAccess.Models
{
    public class ForecastResponse
    {
        [JsonProperty("city")]
        public CityForecast CityName { get; set; }

        public List<InfoForecast> List { get; set; }  
    }
}
