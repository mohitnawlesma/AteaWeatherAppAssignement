using Microsoft.Extensions.DependencyInjection;
using WeatherApp.DataAccessLayer.Interfaces;
using WeatherApp.DataAccessLayer.Repository;

namespace WeatherApp.DataAccessLayer
{
    public static class DataAccessLayerCollectionExtension
    {
        public static void AddDataAccessLayerDependency(this IServiceCollection services)
        {
            services.AddScoped<IWeatherDetailRepository, WeatherDetailRepository>();
        }
    }
}
