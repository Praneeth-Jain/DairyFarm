using System.ComponentModel.DataAnnotations;

namespace DairyFarm.Model
{
    public class AddCattleClass
    {
        [Required]
        public string CowName { get; set; }

        [Required]
        public DateTime DOB { get; set; }

        [Required]
        public string Breed { get; set; }

        [Required]
        public string HealthStatus { get; set; }
    }
}
