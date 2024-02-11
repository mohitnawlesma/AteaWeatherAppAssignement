using Microsoft.AspNetCore.Mvc;
using WeatherApp.Domain.Dtos;
using WeatherApp.Service.Interfaces;

namespace WeatherApp.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherDetailService _service;


        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherDetailService service)
        {
            _logger = logger;
            _service = service;
        }


        [HttpGet(Name = "GetCityWeatherDetails")]
        public List<CityWeatherDetailDto>? GetCityWeatherDetails(string city, string country)
        {
            return _service.GetCityWeatherDetails(city,country);
        }
    }
}
