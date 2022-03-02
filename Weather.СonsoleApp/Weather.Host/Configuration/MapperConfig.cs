using AutoMapper;
using Weather.Host.Configuration.Profiles;

namespace Weather.Host.Configuration
{
    public static class MapperConfig
    {
        public static MapperConfiguration GetConfiguration()
        {
            var configExpression = new MapperConfigurationExpression();

            configExpression.AddProfile<WeatherProfile>();
            

            var config = new MapperConfiguration(configExpression);
            config.AssertConfigurationIsValid();

            return config;
        }
    }
}
