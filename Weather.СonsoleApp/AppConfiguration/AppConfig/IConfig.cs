namespace AppConfiguration.AppConfig
{
    public interface IConfig
    {
        string UrlBase { get;  }

        string UrlWeather { get; }

        string UrlForecast { get; } 

        string ApiKey { get;  }

        string UrlLocationCity { get; }

        string Days { get; } 
    }
}
