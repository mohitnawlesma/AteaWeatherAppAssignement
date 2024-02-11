using WeatherApp.Domain.Entities;

namespace WeatherApp.DataAccessLayer.Interfaces
{
    public interface IWeatherDetailRepository
    {
        public List<CityWeatherDetail> GetWeatherDetails(string city);
        List<CityLocationDetail> GetAllCityLocationDetails();
        bool SaveWeatherDetails(CityWeatherDetail detail);
    }
}
