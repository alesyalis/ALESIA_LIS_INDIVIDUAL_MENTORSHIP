using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Weather.BL.DTOs;
using Weather.DataAccess.Models;

namespace Weather.BL.Mapping.Profiles
{
    public class WeatherHistoryProfile : Profile
    {
        public WeatherHistoryProfile()
        {
            CreateMap<WeatherResponse,WeatherHistory>()
                .ForMember(dto => dto.Temp, src => src.MapFrom(entity => entity.Main.Temp))
                .ForMember(dto => dto.CityName , src => src.MapFrom(entity => entity.Name))
                .ForMember(dto => dto.Id, src => src.Ignore())
                .ReverseMap();
            CreateMap<WeatherHistoryDTO, WeatherHistory>().ReverseMap();
        }
    }
}
