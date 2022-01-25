using System;
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
        public async Task<WeatherResponse> GetWeatherAsync(string cityName)
        {
            try
            {
                var url = string.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&units=metric&appid=8e943ed8b016561c73b8a1920366ef79", cityName);
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                string response;

                using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    response = streamReader.ReadToEnd();
                }

                var weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(response);

                return weatherResponse;
                
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError && ex.Response != null)
                {
                    var resp = (HttpWebResponse)ex.Response;
                    if (resp.StatusCode == HttpStatusCode.NotFound)
                    {
                       Console.WriteLine("\nCity not found, reaped please");
                    }
                }

                var name = Console.ReadLine();
                return await GetWeatherAsync(name);
            }
        }
    }
}
