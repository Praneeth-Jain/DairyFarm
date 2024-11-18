using System.ComponentModel.DataAnnotations;

namespace DairyFarm.Data.Entity
{
    public class ByProduct
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OwnerId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public decimal Quantity { get; set; }

        [Required]
        public decimal Revenue { get; set; }

    }
}
