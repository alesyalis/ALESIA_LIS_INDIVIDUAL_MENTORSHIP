using Weather.BL.DTOs;
using Weather.DataAccess.Models;

namespace Weather.Host.Configuration.Profiles
{
    public class WeatherProfile : AutoMapper.Profile
    {
        public WeatherProfile()
        {
            CreateMap<WeatherResponse, WeatherResponseDTO>();
            CreateMap<TemperatureInfo, TemperatureInfoDTO>();
        }
    }
}
