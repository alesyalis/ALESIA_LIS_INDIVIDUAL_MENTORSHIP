using Microsoft.Extensions.Configuration;
using System.Collections.Specialized;

namespace AppConfiguration.Extentions
{
    public static class ConfigExtentioncs
    {
        public static IConfigurationRoot GetConfigTest(this IConfigurationRoot configurationRoot)
        {
            configurationRoot = new ConfigurationBuilder().AddJsonFile("appsettings.json")
                                                 .Build();
            return configurationRoot;
        }
        public static NameValueCollection GetConfig(this NameValueCollection configurationRoot)
        {
            configurationRoot = System.Configuration.ConfigurationManager.AppSettings;
            return configurationRoot;
        }
    }
}
