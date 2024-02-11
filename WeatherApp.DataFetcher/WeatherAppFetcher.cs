using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WeatherApp.Service.Interfaces;

namespace WeatherApp.DataFetcher
{
    internal class WeatherAppFetcher : BackgroundService
    {
        private readonly IWeatherDetailService _service;

        private readonly IHost _host;


        public WeatherAppFetcher(IHost host, IWeatherDetailService service)
        {
            _host = host;
            _service = service;
        }

        protected async override Task<Task> ExecuteAsync(CancellationToken stoppingToken)
        {
            await _service.FetchAndSaveWeatherData();
            return _host.StopAsync(stoppingToken);
        }
    }
}
