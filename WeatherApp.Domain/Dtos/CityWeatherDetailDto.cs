namespace WeatherApp.Domain.Dtos
{
    public class CityWeatherDetailDto
    {
        public string? Country { get; set; }
        public string? City { get; set; }
        public decimal? Temperature { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
