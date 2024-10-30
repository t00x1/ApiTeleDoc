using System.ComponentModel.DataAnnotations;
namespace  Domain.ModelsDTO
{
    public class FounderDto
    {


        [MaxLength(10)]
        public string? INN { get; set; } = string.Empty;

        [MaxLength(10)]
        public string? Phone { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? LastName { get; set; } = string.Empty; 

        [MaxLength(100)]
        public string? FirstName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Patronymic { get; set; }


        [MaxLength(254)]
        public string? Email { get; set; } = string.Empty;

        public string? ClientINN { get; set; }

   
        public DateTime? DateAdded { get; set; }

        public DateTime? DateUpdated { get; set; }
    }
}

