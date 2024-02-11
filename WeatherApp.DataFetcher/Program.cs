using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WeatherApp.DataFetcher;
using WeatherApp.Domain.Entities;

using WeatherApp.DataAccessLayer;
using WeatherApp.Service;
using AutoMapper;
using WeatherApp.Domain.AutoMapperProfile;

var configuration = new ConfigurationBuilder()
                  .AddJsonFile("appsettings.json", false, true)
                  .Build();

var connectionstring = configuration.GetValue<string>("Database:ConnectionString");
var host = Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((hostContext, configBuilder) =>
        {
            configBuilder
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile(@"C:\\kivu\configuration\appsettings.json", false, true)
                .Build();
        })
       .ConfigureServices(
           services =>
           {
               services.AddScoped(p => new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapperProfile())).CreateMapper());
               services.AddLogging();
               services.AddPooledDbContextFactory<WeatherContext>(options => options.UseSqlServer(connectionstring));
               services.AddHostedService<WeatherAppFetcher>();
               services.AddDataAccessLayerDependency();
               services.AddServiceLayerDependency();
           }).ConfigureLogging(
           log =>
           {
               log.AddFilter("Microsoft", level => level >= LogLevel.Warning);
               log.AddConsole();
           })
       .Build();

await host.RunAsync();
