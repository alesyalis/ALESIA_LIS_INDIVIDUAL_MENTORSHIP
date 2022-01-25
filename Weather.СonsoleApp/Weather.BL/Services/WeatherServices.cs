using Newtonsoft.Json;
using System.IO;
using System.Net;
using Weather.DataAccess.Models;
using System;
using Weather.BL.Services.Abstract;

namespace Weather.BL.Services
{
    public class WeatherServicesv : IWeatherServices
    {
        public void GetWeather(string cityName)
        {
            try
            {
                string url = string.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&units=metric&appid=8e943ed8b016561c73b8a1920366ef79", cityName);
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                string response;

                using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    response = streamReader.ReadToEnd();
                }

                WeatherResponse weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(response);

                Console.WriteLine("В {0}: {1} °C {2} ", weatherResponse.Name, weatherResponse.Main.Temp,
                                                              WeatherComment(weatherResponse.Main.Temp));
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError && ex.Response != null)
                {
                    var resp = (HttpWebResponse)ex.Response;
                    if (resp.StatusCode == HttpStatusCode.NotFound)
                    {
                        Console.WriteLine("City not found");
                    }
                }
            }
        }
        private string WeatherComment(double temp)
        {
            if (temp < 0)
                return "Dress warmly.";
            if (temp >= 0 && temp <= 20)
                return "It's fresh.";
            if (temp > 20 && temp <= 30)
                return "Good weather!";
            if (temp > 30)
                return "It's time to go to the beach";
            else
                return "Nothing";
        }
    }
}
