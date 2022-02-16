using Newtonsoft.Json;

namespace Weather.DataAccess.Models
{
    public class LocationCity
    {
        [JsonProperty("lat")]
        public float LocationLatitude { get; set; }

        [JsonProperty("lon")]
        public float LocationLongitude { get; set; } 
    }
}
