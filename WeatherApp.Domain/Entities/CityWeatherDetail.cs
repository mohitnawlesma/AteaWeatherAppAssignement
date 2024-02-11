﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherApp.Domain.Entities
{
    public class CityWeatherDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CityWeatherDetailId { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public decimal? Temperature { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
