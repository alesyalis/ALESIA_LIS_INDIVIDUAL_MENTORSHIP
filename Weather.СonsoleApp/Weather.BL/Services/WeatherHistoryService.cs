﻿using AutoMapper;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Weather.BL.DTOs;
using Weather.BL.Services.Abstract;
using Weather.DataAccess.Models;
using Weather.DataAccess.Repositories.Abstrdact;

namespace Weather.BL.Services
{
    public class WeatherHistoryService : IWeatherHistoryService
    {
        private readonly IWeatherHistoryRepository _weatherHistoryRepository;
        private readonly IWeatherRepository _weatherRepository;
        private readonly IMapper _mapper;
        private CancellationTokenSource _cancellationTokenSource;

        public WeatherHistoryService(IWeatherHistoryRepository weatherHistoryRepository, IWeatherRepository weatherRepository,
            IMapper mapper)
        {
            _weatherHistoryRepository = weatherHistoryRepository;
            _weatherRepository = weatherRepository;
            _mapper = mapper;
            _cancellationTokenSource = new CancellationTokenSource();   
        }

        //public async Task AddWeatherHistoryAsync(CityDTO city, CancellationTokenSource token)
        //{

        //    var weather = await _weatherRepository.GetWeatherAsync(city.CityName, token);
        //    var weatherHistory = new WeatherHistory
        //    {
        //        Timestamp = DateTime.Now,
        //        CityId = city.Id,
        //        Temp = weather.Main.Temp
        //    };

        //    await _weatherHistoryRepository.CreateAsync(weatherHistory);

        //}

        public async Task BackgroundSaveWeatherAsync(IEnumerable<string> cities)
        {
            var weathers = cities.Select(async x =>
            {
                var weatherRespons = await _weatherRepository.GetWeatherAsync(x, _cancellationTokenSource);
                var weathers = _mapper.Map<WeatherHistory>(weatherRespons);
                weathers.DateTime = DateTime.Now;
                return weathers;

            });

            var result =await Task.WhenAll(weathers);
            
            await _weatherHistoryRepository.BulkSaveAsync(result);
            //await _weatherHistoryRepository.BalkSaveWeatherAsync(result);

        }

        public Task<List<WeatherHistoryDTO>> GetWeatherHistoriesAsync(string cityName, DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }
    }
}
