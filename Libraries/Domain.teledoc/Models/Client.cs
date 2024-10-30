using System.ComponentModel.DataAnnotations;
using Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
namespace Domain.Models
{
    public class Client
    {
        [Key]
        [MaxLength(10), Required]
        public string INN { get; set; } = string.Empty;

        public ClientType Type { get; set; }

        [MaxLength(10), Required]
        public string Phone { get; set; } = string.Empty;

        [MaxLength(254), Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public ClientStatus Status { get; set; }

        public ICollection<Founder> Founders { get; set; } = new List<Founder>();

        [Required]
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime DateUpdated { get; set; } = DateTime.UtcNow;
    }
}
