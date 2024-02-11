using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using System.Net.Http;
using WeatherApp.DataAccessLayer.Interfaces;
using WeatherApp.Domain.Dtos;
using WeatherApp.Domain.Entities;
using WeatherApp.Service.Services;

namespace WeatherApp.Service.Test
{
    [TestFixture]
    public class WeatherDetailServiceTest
    {
        protected ILogger<WeatherDetailService> _logger;
        protected IMapper _mapper;
        protected IWeatherDetailRepository _repository;
        protected IConfiguration _configuration;

        [SetUp]
        public void SetUp()
        {
            _mapper = Substitute.For<IMapper>();
            _logger = Substitute.For<ILogger<WeatherDetailService>>();
            _repository = Substitute.For<IWeatherDetailRepository>();
            _configuration = Substitute.For<IConfiguration>();
        }

        [Test]
        public void GetCityWeatherDetails_Should_Return_Correct_Result()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CityWeatherDetail, CityWeatherDetailDto>());
            var mapper = config.CreateMapper();
            _repository.GetWeatherDetails("Latvia").Returns(new List<CityWeatherDetail>() { new CityWeatherDetail() { Country = "Latvia", Temperature = (decimal)-8.101 } });
            _mapper.Map<List<CityWeatherDetailDto>>(new List<CityWeatherDetail>() { new CityWeatherDetail() { Country = "Latvia", Temperature = (decimal)-8.101 } }).Returns(new List<CityWeatherDetailDto>() { new CityWeatherDetailDto() { Country = "Latvia", Temperature = (decimal)-8.101 } });
            
            var service = new WeatherDetailService(_logger, mapper, _repository, _configuration);
            var result = service.GetCityWeatherDetails("Latvia");
           
            result.Count.Should().Be(1);
            result.First().Country.Should().Be("Latvia");
        }

        [Test]
        public void GetCityWeatherDetails_Should_Return_Exception()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CityWeatherDetail, CityWeatherDetailDto>());
            var mapper = config.CreateMapper();
            _repository.GetWeatherDetails("Latvia").Returns(x => { throw new Exception(); });
             
            var service = new WeatherDetailService(_logger, mapper, _repository, _configuration);
            var result = service.GetCityWeatherDetails("Latvia");

            result.Should().BeNull();
        }
    }
}
