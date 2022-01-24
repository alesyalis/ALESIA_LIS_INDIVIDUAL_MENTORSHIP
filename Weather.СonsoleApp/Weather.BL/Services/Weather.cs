using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Weather.DataAccess.Models;

namespace Weather.BL.Services
{
    public class Weather
    {
        public static void GetWeather(string cityName)
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

                System.Console.WriteLine("В {0}: {1} °C {2} ", weatherResponse.Name, weatherResponse.Main.Temp,
                                                              WeatherComment(weatherResponse.Main.Temp));
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError && ex.Response != null)
                {
                    var resp = (HttpWebResponse)ex.Response;
                    if (resp.StatusCode == HttpStatusCode.NotFound)
                    {
                        System.Console.WriteLine("City not found");
                    }
                }
            }
        }
        public static string WeatherComment(double temp)
        {
            var desc = "";
            if (temp < 0)
                return desc = "Dress warmly.";
            if (temp >= 0 && temp <= 20)
                return desc = "It's fresh.";
            if (temp > 20 && temp <= 30)
                return desc = "Good weather!";
            if (temp > 30)
                return desc = "It's time to go to the beach";
            else
                return desc = "Nothing";
        }
    }
}
