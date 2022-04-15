using Hangfire;
using Hangfire.Storage;
using System.Collections.Generic;
using System.Linq;
using Weather.BL.Configuration;
using Weather.BL.Services.Abstract;

namespace Weather.BL.Services
{
    public class BackgroundJobService : IBackgroundJobService
    {
        private readonly IWeatherHistoryService _weatherHistoryService;
        private readonly IRecurringJobManager _recurringJobManager;
        private readonly JobStorage _jobStorage;

        public BackgroundJobService(IWeatherHistoryService weatherHistoryService,
            IRecurringJobManager recurringJobManager,
            JobStorage jobStorage)
        {
            _weatherHistoryService = weatherHistoryService;
            _jobStorage = jobStorage;
            _recurringJobManager = recurringJobManager;
        }
        public void UpdateJobs(IEnumerable<CityOption> cityOptions)
        {
            var currentJobs = _jobStorage.GetConnection().GetRecurringJobs()
                .Select(x => new { Name = x.Id, Timeout = x.Cron }).ToList();

            var dictionarunewJobs = cityOptions.GroupBy(x => Cron.MinuteInterval(x.TimeOut))
                .ToDictionary(k => k.Key, v => v.Select(x => x.CityName)).ToList();
            var newJobs = dictionarunewJobs.Select(x => new {Name = GetJobName(x.Value), Timeout = x.Key})
                .ToList();
             var remoutJobs = currentJobs.Where(c => !newJobs.Any(n => n.Name == c.Name && n.Timeout == c.Timeout)).ToList();
            dictionarunewJobs.ForEach(x => _recurringJobManager.AddOrUpdate(GetJobName(x.Value),
                () => _weatherHistoryService.BackgroundSaveWeatherAsync(x.Value), x.Key));
        }
        private string GetJobName(IEnumerable<string> cities)
        {
            return cities.OrderBy(c => c).Aggregate((result, next) => $"{result}; {next}").ToLower();
        }
    }
}
