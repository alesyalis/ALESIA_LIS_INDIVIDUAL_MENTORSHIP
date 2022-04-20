﻿using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Weather.BL.Mapping
{
    public static class MapperConfigurationProvider
    {
        public static MapperConfigurationExpression MapperConfigurationExpression { get; }

        static MapperConfigurationProvider()
        {
            MapperConfigurationExpression = new MapperConfigurationExpression();
            MapperConfigurationExpression.AddMaps(Assembly.GetExecutingAssembly());
        }

        public static MapperConfiguration GetConfig()
        {
            return new MapperConfiguration(MapperConfigurationExpression);
        }

        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            var mapper = GetConfig().CreateMapper();

            return services.AddSingleton(mapper);
        }
    }
}
