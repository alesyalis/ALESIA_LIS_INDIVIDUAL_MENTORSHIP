using System.Collections.Generic;

namespace Weather.BL.Configuration
{
    public class BackgroundJobConfiguration
    {
        public IEnumerable<CityOption> CityOptions { get; set; }
    }
}