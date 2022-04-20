namespace Weather.DataAccess.Models
{
    public class WeatherResponse
    {
       // public int Id { get; set; }
        public TemperatureInfo Main { get; set; }

        public string Name { get; set; }

        public long LeadTime { get; set; }

        public int CountSuccessfullRequests { get; set; }

        public int CountFailedRequests { get; set; }

        public string Status { get; set; }
        
        public int Canceled { get; set; }   
    }
}
