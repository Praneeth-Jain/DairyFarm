
using System.ComponentModel.DataAnnotations;

namespace DairyFarm.Data.Entity
{
    public class Food
    {
        [Key]
        public int FoodId { get; set; }

        [Required]
        public int OwnerId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal PricePerUnit { get; set; }


    }
}
