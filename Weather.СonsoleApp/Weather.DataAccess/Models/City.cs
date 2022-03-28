using System;
using System.Collections.Generic;
using System.Text;

namespace Weather.DataAccess.Models
{
    public class City
    {
        public int Id { get; set; }

        public string CityName { get; set; }

        public ICollection<WeatherHistory> WeatherHistories { get; set; }   
    }
}
