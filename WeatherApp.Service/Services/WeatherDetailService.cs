using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http.Json;
using WeatherApp.DataAccessLayer.Interfaces;
using WeatherApp.Domain.Dtos;
using WeatherApp.Domain.Entities;
using WeatherApp.Domain.Models;
using WeatherApp.Service.Interfaces;

namespace WeatherApp.Service.Services
{
    public class WeatherDetailService : IWeatherDetailService
    {
        protected readonly ILogger<WeatherDetailService> _logger;
        protected readonly IMapper _mapper;
        protected readonly IWeatherDetailRepository _repository;
        protected readonly IConfiguration _configuration;


        public WeatherDetailService(ILogger<WeatherDetailService> logger, IMapper mapper, IWeatherDetailRepository repository, IConfiguration configuration)
        {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
            _configuration = configuration; 
        }

        public List<CityWeatherDetailDto>? GetCityWeatherDetails(string city)
        {
            try
            {
                var data = _repository.GetWeatherDetails(city);
                if (data is not null && data.Any() & _mapper is not null)
                {
                    return _mapper.Map<List<CityWeatherDetailDto>>(data);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetCityWeatherDetails: {ex.Message}");
                return null;
            }
            return new();
        }

        public async Task<bool?> FetchAndSaveWeatherData()
        {
            try
            {
                var cityLocationData = _repository.GetAllCityLocationDetails();
                if (cityLocationData is not null && cityLocationData.Any())
                {
                    foreach (var cityLocation in cityLocationData)
                    {
                        WeatherApiResponse? response = null;
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(_configuration["WeatherApiBaseUrl"]);
                            response = await client.GetFromJsonAsync<WeatherApiResponse>($"?lat={cityLocation.Latitude}&lon={cityLocation.Longitude}&appid={_configuration["WeatherApiKey"]}&units=metric");
                        }

                        if (response?.main?.temp is not null)
                        {
                            var cityWeather = new CityWeatherDetail()
                            {
                                Country = cityLocation.Country,
                                City = cityLocation.City,
                                Temperature = response.main.temp,
                                UpdatedDate = DateTime.Now
                            };
                            _repository.SaveWeatherDetails(cityWeather);
                        }
                    }                    
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetCityWeatherDetails: {ex.Message}");
            }
            return false;
        }
    }
}
