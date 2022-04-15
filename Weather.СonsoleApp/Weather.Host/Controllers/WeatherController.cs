using Microsoft.AspNetCore.Mvc;
using Weather.BL.DTOs;
using Weather.BL.Exceptions;
using Weather.BL.Services.Abstract;

namespace Weather.Host.Controllers
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

        [HttpGet("getWeather")]
        public async Task<ActionResult<WeatherResponseDTO>> GetWeather([FromQuery] string name)
        {
            try
            {
                var result =  await _weatherService.GetWeatherAsync(name);
                return Ok(result);
            }
            catch (ValidationException)
            {
                return NotFound();
            }
        }

        [HttpGet("getWeatherForecast")]
        public async Task<ActionResult<WeatherResponseDTO>> GetWeatherForecast([FromQuery] string name, [FromQuery] int days)
        {
            try
            {
                var result = await _weatherService.GetForecastAsync(name, days);
                return Ok(result);
            }
            catch (ValidationException)
            {
                return NotFound();
            }
        }
    }
}
