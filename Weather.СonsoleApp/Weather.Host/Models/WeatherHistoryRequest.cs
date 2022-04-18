namespace Weather.Host.Models
{
    public class WeatherHistoryRequest
    {
        public string CityName { get; set; }    
        public DateTime  DateTimeFrom { get; set; }
        public DateTime DateTimeTo { get; set; }

    }
}
