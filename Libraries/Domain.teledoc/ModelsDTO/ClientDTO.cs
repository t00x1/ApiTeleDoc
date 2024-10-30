using System;
using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.ModelsDTO
{
    public class ClientDto
    {
        [MaxLength(10)]
        public string? INN { get; set; } 

        public int? Type { get; set; }

        [MaxLength(10)]
        public string? Phone { get; set; }
        [MaxLength(254)]
        public string? Email { get; set; }

        public int? Status { get; set; }

        public DateTime? DateAdded { get; set; } 

        public DateTime? DateUpdated { get; set; }
    }
}