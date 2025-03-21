﻿
using System.ComponentModel.DataAnnotations;


namespace GoWheels_WebAPI.Models.DTOs
{
    public class CompanyDTO
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public IFormFile? IconImage { get; set; }
        public List<int> CarTypeIds { get; set; } = new List<int>();
    }
}
