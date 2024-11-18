using System.ComponentModel.DataAnnotations;

namespace DairyFarm.Data.Entity
{
    public class Subscriptions
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OwnerId { get; set; }

        [Required]
        public string Tier { get; set; }

        [Required]
        public DateTime StartDate { get; set;}


        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string Status { get; set; }


    }
}
