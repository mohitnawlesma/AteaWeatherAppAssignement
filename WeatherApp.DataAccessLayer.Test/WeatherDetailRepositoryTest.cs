using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using WeatherApp.DataAccessLayer.Repository;
using WeatherApp.Domain.Entities;

namespace WeatherApp.DataAccessLayer.Test
{
    [TestFixture]
    public class WeatherDetailRepositoryTest
    {
        protected IDbContextFactory<WeatherContext> _contextFactory;
        protected ILogger<WeatherDetailRepository> _logger;

        [SetUp]
        public void SetUp()
        {
            _logger = Substitute.For<ILogger<WeatherDetailRepository>>();
            _contextFactory = Substitute.For<IDbContextFactory<WeatherContext>>();
        }

        [Test]
        public void  GetCityWeatherDetails_Should_Return_Correct_Result()
        {
            var options = new DbContextOptionsBuilder<WeatherContext>()
                               .UseInMemoryDatabase(databaseName: "SomeDatabaseInMemory")
                               .Options;
            var newCityWeatherDetailId = Guid.NewGuid();
            var newCityWeatherDetailId1 = Guid.NewGuid();

            // Insert seed data into the database using an instance of the context
            using (var context = new WeatherContext(options))
            {
                context.CityWeatherDetail.Add(new CityWeatherDetail
                {
                    CityWeatherDetailId = newCityWeatherDetailId,
                    City = "Riga",
                    Country = "Latvia",
                    Temperature = (decimal)-8.1
                });
                context.CityWeatherDetail.Add(new CityWeatherDetail
                {
                    CityWeatherDetailId = newCityWeatherDetailId1,
                    City = "Berlin",
                    Country = "DE",
                    Temperature = (decimal)-4.1
                });
                context.SaveChanges();
            }

            // Now the in-memory db already has data, we don't have to seed everytime the factory returns the new DbContext:
            _contextFactory.CreateDbContext().Returns(new WeatherContext(options));

            // ACT
            var ks = new WeatherDetailRepository(_logger,  _contextFactory);
            var result = ks.GetWeatherDetails("Riga");

            // ASSERT
            result.Count.Should().Be(1);
            result.First().Country.Should().Be("Latvia");
            result.First().Temperature.Should().Be((decimal)-8.1);


        }
    }
}
