using AutoMapper;
using Hangfire;
using Hangfire.Storage;
using Microsoft.Extensions.Logging;
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
    public class BackgroundJobService : IBackgroundJobService
    {
        private readonly IWeatherHistoryRepository _weatherHistoryRepository;
        private readonly IRecurringJobManager _recurringJobManager;
        private readonly IWeatherRepository _weatherRepository;
        private readonly JobStorage _jobStorage;
        private readonly IMapper _mapper;
        private CancellationTokenSource _cancellationTokenSource;
        private readonly ILogger<BackgroundJobService> _logger;

        public BackgroundJobService(IWeatherHistoryRepository weatherHistoryRepository,
            IRecurringJobManager recurringJobManager,
            IWeatherRepository weatherRepository,
            JobStorage jobStorage,
            IMapper mapper,
            ILogger<BackgroundJobService> logger)
        {
            _weatherHistoryRepository = weatherHistoryRepository;
            _weatherRepository = weatherRepository;
            _jobStorage = jobStorage;
            _recurringJobManager = recurringJobManager;
            _mapper = mapper;
            _cancellationTokenSource = new CancellationTokenSource();
            _logger = logger;
        }
        public Task UpdateJob(IEnumerable<CityOptionDTO> cityOptions)
        {
            var currentArrayCitties = _jobStorage.GetConnection().GetRecurringJobs().Select(x => x.Id).ToList();
            var newArrayCities = cityOptions.Select(x => x.CityName.ToLower()).ToList();
            currentArrayCitties.Except(newArrayCities).ToList().ForEach(x => _recurringJobManager.RemoveIfExists(x));

            cityOptions.ToList().ForEach(x => _recurringJobManager.AddOrUpdate(x.CityName.ToLower(), () => GetWeather(x.CityName), Cron.MinuteInterval(x.Timeout)));
            return Task.CompletedTask;
        }

        public async Task GetWeather(string cityName)
        {
            try
            {
                var weatherdto = _weatherRepository.GetWeatherAsync(cityName, _cancellationTokenSource);
                var weather = _mapper.Map<WeatherHistory>(weatherdto);
                weather.Timestamp = DateTime.Now;
                await _weatherHistoryRepository.CreateAsync(weather);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

        }
    }
}
