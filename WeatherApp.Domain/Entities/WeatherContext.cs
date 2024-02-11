using Microsoft.EntityFrameworkCore;

namespace WeatherApp.Domain.Entities
{
    public class WeatherContext : DbContext
    {
       // public WeatherContext() { }
        public WeatherContext(DbContextOptions<WeatherContext> options) : base(options) { }

        
        public DbSet<CityWeatherDetail> CityWeatherDetail { get; set; }
        public DbSet<CityLocationDetail> CityLocationDetail { get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlServer("Data Source=DESKTOP-L4LLJTK;Initial Catalog=WeatherDetails;User id=sa;Password =sqlserver;Command Timeout=0;TrustServerCertificate=True");

        //public WeatherContext CreateDbContext(string[] args)
        //{
        //    var optionsBuilder = new DbContextOptionsBuilder<WeatherContext>();
        //    optionsBuilder.UseSqlServer("Data Source=DESKTOP-L4LLJTK;Initial Catalog=WeatherDetails;User id=sa;Password =sqlserver;Command Timeout=0;TrustServerCertificate=True");

        //    return new WeatherContext(optionsBuilder.Options);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CityWeatherDetail>().ToTable("CityWeatherDetail");
            modelBuilder.Entity<CityLocationDetail>().ToTable("CityLocationDetail");

        }
    }
}
