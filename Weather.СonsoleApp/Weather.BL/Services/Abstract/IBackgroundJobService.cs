using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Weather.BL.DTOs;

namespace Weather.BL.Services.Abstract
{
    public interface IBackgroundJobService
    {
        Task UpdateJob(IEnumerable<CityOptionDTO> cityOptions);
    }
}
