using System;
using System.Collections.Generic;
using System.Text;
using Weather.DataAccess.Models;

namespace Weather.BL.DTOs
{
    public class WeatherResponseDTO
    {
        public TemperatureInfoDTO Main { get; set; }

        public string Name { get; set; }
    }
}
