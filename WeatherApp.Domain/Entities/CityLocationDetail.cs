using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WeatherApp.Domain.Entities
{
    public class CityLocationDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CityLocationDetailId { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public DateTime? UpdatedDate { get; set; }

    } 
}
