using System.ComponentModel.DataAnnotations;

namespace DairyFarm.Data.Entity
{
    public class Income
    {
        [Key]
        public int Id { get; set; }


        public int? CowId { get; set; }


        [Required]
        public int OwnerId { get; set; }


        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }=DateTime.Now;
    }
}
