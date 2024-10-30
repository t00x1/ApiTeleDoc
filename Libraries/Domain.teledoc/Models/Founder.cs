using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Founder
    {
        [Key]
        [MaxLength(10), Required]
        public string INN { get; set; } = string.Empty;

    
        [MaxLength(10), Required]
        public string Phone { get; set; } = string.Empty;

        [MaxLength(254), Required]
        public string Email { get; set; } = string.Empty;
        [ForeignKey("Client"), Required]
        public string ClientINN { get; set; } = string.Empty;
        [Required]
        public Client Client { get; set; } = null!;

        [MaxLength(100), Required]
        public string LastName { get; set; } = string.Empty; 

        [MaxLength(100), Required]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Patronymic { get; set; }

        [Required]
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime DateUpdated { get; set; } = DateTime.UtcNow;
    }
}