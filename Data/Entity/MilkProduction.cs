using System.ComponentModel.DataAnnotations;

namespace DairyFarm.Data.Entity
{
    public class MilkProduction
    {
        [Key]
        public int ProductionId { get; set; }

        [Required]
        public int CowId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public decimal MilkYieldLitres { get; set; }


    }
}
