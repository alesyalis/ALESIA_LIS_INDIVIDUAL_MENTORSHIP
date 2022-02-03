using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Weather.IntegrationTest
{
    public class IntegrationTest 
    {
        readonly HttpClient _client;

        public IntegrationTest()
        {
            _client = new HttpClient(); 
        }
        [Fact]
        public async Task Get_Api()
        {

            string url = "https://api.openweathermap.org/data/2.5/weather?q=Minsk&units=metric&appid=8e943ed8b016561c73b8a1920366ef79";

            using (var response = _client.GetAsync(url))
            {
                Assert.Equals("Su", response.Result);
                Assert.AreEqual(HttpStatusCode.OK, response.Status);
            }
        }
    }
}
