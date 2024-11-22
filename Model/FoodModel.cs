using System.ComponentModel.DataAnnotations;

namespace DairyFarm.Model
{
    public class FoodClass
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public decimal PricePerUnit { get; set; }
    }
}
