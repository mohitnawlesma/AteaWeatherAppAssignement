using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Diagnostics.Tracing;
using WeatherApp.DataAccessLayer.Interfaces;
using WeatherApp.Domain.Entities;

namespace WeatherApp.DataAccessLayer.Repository
{
    public class WeatherDetailRepository : IWeatherDetailRepository
    {
        protected readonly IDbContextFactory<WeatherContext> _contextFactory;
        protected readonly ILogger<WeatherDetailRepository> _logger;

        public WeatherDetailRepository(ILogger<WeatherDetailRepository> logger,IDbContextFactory<WeatherContext> contextFactory)
        {
            _logger = logger;
            _contextFactory = contextFactory;
        }
        public List<CityWeatherDetail> GetWeatherDetails(string city, string country)
        {
            try
            {
                if (_contextFactory is not null)
                {
                    using (WeatherContext context = _contextFactory.CreateDbContext())
                    {
                        return context.CityWeatherDetail.Where(p => p.City == city && p.Country == country).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetWeatherDetails:{ex.Message}");
            }
            return new();
        }

        public bool SaveWeatherDetails(CityWeatherDetail detail)
        {
            try
            {
                if (_contextFactory is not null)
                {
                    using (WeatherContext context = _contextFactory.CreateDbContext())
                    {
                        context.CityWeatherDetail.Add(detail);
                        context.SaveChanges();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetWeatherDetails:{ex.Message}");
            }
            return false;
        }

        public List<CityLocationDetail> GetAllCityLocationDetails()
        {
            try
            {
                if (_contextFactory is not null)
                {
                    using (WeatherContext context = _contextFactory.CreateDbContext())
                    {
                        return context.CityLocationDetail.ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetWeatherDetails:{ex.Message}");
            }
            return new();
        }
    }
}
