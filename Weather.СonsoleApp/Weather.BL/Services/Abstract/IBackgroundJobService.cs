using System.Collections.Generic;
using Weather.BL.Configuration;

namespace Weather.BL.Services.Abstract
{
    public interface IBackgroundJobService
    {
        void UpdateJobs(IEnumerable<CityOption> cityOptions);
    }
}
