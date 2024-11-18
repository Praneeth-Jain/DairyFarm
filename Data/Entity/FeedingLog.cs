using System.ComponentModel.DataAnnotations;

namespace DairyFarm.Data.Entity
{
    public class FeedingLog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CowId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string FoodName { get; set; }

        [Required]
        public decimal QuantityKG { get; set; }

        [Required]
        public decimal TotalCost {  get; set; }

    }
}
