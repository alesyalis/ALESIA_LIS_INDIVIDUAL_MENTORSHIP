using Weather.Host.Infrastructure;

namespace Weather.Host.Extension
{
    public static class StartupFilterRegistration
    {
        public static void AddStartupFilter(this IServiceCollection services)
        {
            services.AddTransient<IStartupFilter, BackgroundjobFilter>();
        }
    }
}
