using System.ComponentModel.DataAnnotations;

namespace DairyFarm.Model
{
    public class ExpenseClass
    {
        [Required]
        public string Category { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
