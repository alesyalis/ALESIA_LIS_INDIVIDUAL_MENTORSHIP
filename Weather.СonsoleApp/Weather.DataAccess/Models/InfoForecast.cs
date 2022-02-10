using System;
using System.Collections.Generic;
using System.Text;

namespace Weather.DataAccess.Models
{
    public class InfoForecast
    {
        public ForecastDescription Main { get; set; }
        public DateTime Dt_txt { get; set; }
    }


}

