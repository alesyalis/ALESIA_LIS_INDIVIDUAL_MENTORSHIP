﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Weather.DataAccess.Models;

namespace Weather.BL.Mapping.Profiles
{
    public class WeatherHistoryProfile : Profile
    {
        public WeatherHistoryProfile()
        {
            CreateMap<WeatherHistory, WeatherResponse>()
                //.ForMember(dto => dto.Main.Temp, src => src.MapFrom(entity => entity.Temp))
                .ForMember(dto => dto.Id, src => src.MapFrom(entity => entity.Id))
                .ReverseMap();
        }
    }
}
