using System.ComponentModel.DataAnnotations;

namespace DairyFarm.Model
{

    public class FeedLogModel
    {

        [Required(ErrorMessage = "Cow ID is required.")]
        public int CowId { get; set; }

        [Required(ErrorMessage = "Food Type is required.")]
        public string FoodType { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(0.1, double.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
        public decimal Quantity { get; set; }

        public string Notes { get; set; }
        public DateTime FeedDate { get; set; } = DateTime.Now;

        public List<FoodNameViewModel> FoodNames { get; set; }=new List<FoodNameViewModel>();
    }

    public class FoodNameViewModel
    {
        public string Name { get; set; }

        public decimal PricePerKg { get; set; }
    }
}



