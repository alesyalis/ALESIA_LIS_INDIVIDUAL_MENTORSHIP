﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Weather.DataAccess.Models
{
    public class WeatherResponse
    {
        public TemperatureInfo Main { get; set; }

        public string Name { get; set; }
    }
}
