using System.ComponentModel.DataAnnotations;

namespace DairyFarm.Model
{
    public class ByproductClass
    {

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
