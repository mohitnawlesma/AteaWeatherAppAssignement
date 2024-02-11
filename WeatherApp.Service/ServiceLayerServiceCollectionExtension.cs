using Microsoft.Extensions.DependencyInjection;
using WeatherApp.Service.Interfaces;
using WeatherApp.Service.Services;

namespace WeatherApp.Service
{
    public static class ServiceLayerServiceCollectionExtension
    {
        public static void AddServiceLayerDependency(this IServiceCollection services)
        {
            services.AddScoped<IWeatherDetailService, WeatherDetailService>();
        }
    }
}
