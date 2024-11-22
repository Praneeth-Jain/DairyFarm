using System.ComponentModel.DataAnnotations;

namespace DairyFarm.Model
{
    public class OwnerloginClass
    {
        [Required]
        [EmailAddress]
        public string OwnerEmail { get; set; }

        [Required]
        public string OwnerPassword { get; set; }
    }
}
