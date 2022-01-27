namespace Weather.DataAccess.Models
{
    public class WeatherResponse
    {
        public TemperatureInfo Main { get; set; }

        public string Name { get; set; }

        public string ErrorMessage { get; set; }
    }
}
