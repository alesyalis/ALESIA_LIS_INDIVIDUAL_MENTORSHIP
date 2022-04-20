using AutoMapper;
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

        }

        public async Task<List<WeatherHistoryDTO>> GetWeatherHistoriesAsync(string cityName, DateTime dateTimeFrom, DateTime dateTimeTo)
        {
            var weatherHistories = await _weatherHistoryRepository.GetWeatherHistoriesAsync(cityName, dateTimeFrom, dateTimeTo);
            return _mapper.Map<List<WeatherHistoryDTO>>(weatherHistories);
        }
    }
}