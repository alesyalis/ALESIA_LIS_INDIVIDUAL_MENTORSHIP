namespace AppConfiguration.AppConfig
{
    public class Config : IConfig
    {
        public string UrlBase { get; set; }

        public string UrlWeather { get; set; }

        public string UrlForecast { get; set; }

        public string ApiKey { get; set; }

        public string UrlLocationCity { get; set; }

        public int Days { get; set; }

        public bool IsDebug { get; set; }
    }
}
