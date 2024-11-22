using System.ComponentModel.DataAnnotations;

namespace DairyFarm.Model
{
    public class MilkProductionModel
    {
        [Required(ErrorMessage = "Cow ID is required.")]
        public int CowId { get; set; }

        [Required(ErrorMessage = "Production Date is required.")]
        public DateTime ProductionDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(0.1, double.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
        public decimal Quantity { get; set; }

    }
}
