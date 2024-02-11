namespace WeatherApp.Domain.Models
{
    public class WeatherApiResponse
    {
        public MainData main { get; set; }
    }

    public class MainData
    {
        public decimal temp { get; set; }
    }
}
