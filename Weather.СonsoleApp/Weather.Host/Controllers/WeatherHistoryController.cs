using Microsoft.AspNetCore.Mvc;
using Weather.BL.DTOs;
using Weather.BL.Exceptions;
using Weather.BL.Services.Abstract;
using Weather.Host.Models;

namespace Weather.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherHistoryController : ControllerBase
    {
        private readonly IWeatherHistoryService _weatherService;

        public WeatherHistoryController(IWeatherHistoryService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet("getWeatherHistory")]
        public async Task<ActionResult<WeatherResponseDTO>> GetWeather([FromQuery] WeatherHistoryRequest request)
        { try
            {
                var result = await _weatherService.GetWeatherHistoriesAsync(request.CityName, request.DateTimeFrom, request.DateTimeTo);
                return Ok(result);
            }
            catch (ValidationException)
            {
                return NotFound();
            }
        }

    
    }
}
