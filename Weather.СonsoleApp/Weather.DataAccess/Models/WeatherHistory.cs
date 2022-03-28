﻿using System;

namespace Weather.DataAccess.Models
{
    public class WeatherHistory
    {
        public int Id { get; set; } 

        public int CityId { get; set; }

        public DateTime Timestamp { get; set; }

        public double Temp { get; set; }

        public City City { get; set; }  
    }
}
