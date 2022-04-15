using System;

namespace Weather.DataAccess.Models
{
    public class WeatherHistory
    {
        public int Id { get; set; } 

        public string CityName { get; set; }

        public DateTime DateTime { get; set; }

        public double Temp { get; set; }

    }
}
