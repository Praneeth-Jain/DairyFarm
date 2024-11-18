using System.ComponentModel.DataAnnotations;

namespace DairyFarm.Data.Entity
{
    public class Cows
    {
        [Key]
        public int CowId { get; set; }

        [Required]
        public int OwnerId { get; set; }

        [Required]
        public string CowName { get; set; }

        [Required]
        public  DateTime DOB { get; set; }

        [Required]
        public string Breed { get; set; }

        public DateTime PurschasedDate { get; set; }

        public string HealthStatus { get; set; }
    }
}
