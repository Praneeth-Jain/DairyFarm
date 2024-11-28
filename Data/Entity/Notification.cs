using System.ComponentModel.DataAnnotations;

namespace DairyFarm.Data.Entity
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OwnerId { get; set; }

        [Required]
        public int? CowId { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public DateTime TriggerDate { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public DateTime LastModified { get; set; }

    }
}
