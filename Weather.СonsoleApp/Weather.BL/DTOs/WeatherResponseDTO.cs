using System;
using System.Collections.Generic;
using System.Text;

namespace Weather.BL.DTOs
{
    public class WeatherResponseDTO
    {
        public TemperatureInfoDTO Main { get; set; }

        public string Name { get; set; }

        public long LeadTime { get; set; }

        public int CountSuccessfullRequests { get; set; }

        public int CountFailedRequests { get; set; }

        public string Status { get; set; }

        public int Canceled { get; set; }
    }
}
