using AutoMapper;
using WeatherApp.Domain.Dtos;
using WeatherApp.Domain.Entities;

namespace WeatherApp.Domain.AutoMapperProfile
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CityWeatherDetail, CityWeatherDetailDto>().ReverseMap();
        }
    }
}
