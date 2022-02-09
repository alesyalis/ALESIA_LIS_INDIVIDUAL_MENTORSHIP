using System;
using System.Collections.Generic;
using System.Text;

namespace Weather.DataAccess.Models
{
    public class ForecastResponse
    {
        public CityForecast City { get; set; }

        public List<InfoForecast> List { get; set; }  


    }
}
