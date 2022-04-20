using Hangfire;
using Microsoft.Extensions.Options;
using Weather.BL.Configuration;
using Weather.BL.Services.Abstract;

namespace Weather.Host.Infrastructure
{
    public class BackgroundjobFilter : IStartupFilter
    {
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IOptionsMonitor<BackgroundJobConfiguration> _configBackgroundJob;

        public BackgroundjobFilter(IBackgroundJobClient backgroundJobClient, IOptionsMonitor<BackgroundJobConfiguration> configBackgroundJob)
        {
            _backgroundJobClient = backgroundJobClient; 
            _configBackgroundJob = configBackgroundJob; 
        }
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {   
            _configBackgroundJob.OnChange(c => EnqueueBackgroundJob());
            EnqueueBackgroundJob();
            return next;
        }
        private void EnqueueBackgroundJob()
        {
            _backgroundJobClient.Enqueue<IBackgroundJobService>(x => x.UpdateJobs(_configBackgroundJob.CurrentValue.CityOptions));
        }
    }
}
