﻿using GoWheels_WebAPI.Models.Entities;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GoWheels_WebAPI.Models.DTOs
{
    public class CarTypeDTO : BaseModelDTO
    {
        [Required]
        public required string Name { get; set; }

        [JsonPropertyOrder(100)]
        public List<CarTypeDetailDTO> carTypeDetail { get; set; } = new List<CarTypeDetailDTO>();

        public ICollection<PostDTO> Posts { get; set; } = new List<PostDTO>();
        public List<int> CompanyIds { get; set; } = new List<int>();
    }
}
