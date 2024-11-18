using System.ComponentModel.DataAnnotations;

namespace DairyFarm.Data.Entity
{
    public class Owners
    {
        [Key]
        public int OwnerId { get; set; }

        [Required]
        public string OwnerName { get; set; }

        [Required,EmailAddress]
        public string OwnerEmail { get; set; }

        [Required]
        public string OwnerPhone { get; set; }

        [Required]
        public string OwnerPassword { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }= DateTime.Now;

    }
}
