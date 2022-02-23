namespace AppConfiguration.AppConfig
{
    public interface IConfig
    {
        string UrlBase { get; set; }

        string UrlWeather { get; set; }

        string UrlForecast { get; set; } 

        string ApiKey { get; set; }

        string UrlLocationCity { get; set; }

        int Days { get; set; }

        bool IsDebug { get; set; }
    }
}
