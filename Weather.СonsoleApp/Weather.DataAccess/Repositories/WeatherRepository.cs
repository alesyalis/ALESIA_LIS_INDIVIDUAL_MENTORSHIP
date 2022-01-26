using System.Net;
using System.Threading.Tasks;
using Weather.DataAccess.Repositories.Abstrdact;
using Newtonsoft.Json;
using System.IO;
using Weather.DataAccess.Models;

namespace Weather.DataAccess.Repositories
{
    public class WeatherRepository : IWeatherRepositoty
    {
        public async Task<WeatherResponse> GetWeatherAsync(string cityName, string key)
        {
            var responseWeather = string.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&units=metric&appid={1}", cityName, key);
              
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(responseWeather);

            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            string response;

            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                response = streamReader.ReadToEnd();
            }

            var weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(response);

            return weatherResponse;
        }
    }
}
