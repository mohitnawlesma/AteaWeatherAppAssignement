using Microsoft.EntityFrameworkCore;
using WeatherApp.Domain.Entities;
using WeatherApp.DataAccessLayer;
using WeatherApp.Service;
using WeatherApp.Domain.AutoMapperProfile;
using AutoMapper;
using Serilog;

public partial class Program
{
    public static void Main(string[] args)
    {
        try
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration
            .AddJsonFile("appsettings.json", false, true)
            .AddEnvironmentVariables();

            Log.Logger = new LoggerConfiguration().CreateBootstrapLogger();
            builder.Host.UseSerilog(((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration)));
            builder.Services.AddScoped(p => new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapperProfile())).CreateMapper());
            var connectionstring = builder.Configuration.GetValue<string>("Database:ConnectionString");
            builder.Services.AddPooledDbContextFactory<WeatherContext>(options => options.UseSqlServer(connectionstring));


            builder.Services.AddControllers();
            builder.Services.AddDataAccessLayerDependency();
            builder.Services.AddServiceLayerDependency();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
        catch (Exception)
        {
        }       
    }
}
