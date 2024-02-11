using WeatherApp.Domain.Dtos;

namespace WeatherApp.Service.Interfaces
{
    public interface IWeatherDetailService
    {
        public List<CityWeatherDetailDto>? GetCityWeatherDetails(string city);
        Task<bool?> FetchAndSaveWeatherData();
    }
}
