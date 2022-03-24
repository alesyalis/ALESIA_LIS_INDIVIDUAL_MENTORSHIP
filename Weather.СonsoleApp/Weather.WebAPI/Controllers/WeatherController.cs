using Microsoft.AspNetCore.Mvc;
using Weather.BL.Services.Abstract;

namespace Weather.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;
        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet("getCurrentWeather")]
        public async Task<IActionResult> GetCurrentWeather([FromQuery] string name)
        {
            return Ok(await _weatherService.GetWeatherAsync(name));
        }

        [HttpGet("getWeatherForecast")]
        public async Task<IActionResult> GetWeatherForecast([FromQuery] string name, int days)
        {
            return Ok(await _weatherService.GetForecastAsync(name, days));
        }
    }
}