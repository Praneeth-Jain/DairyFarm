using System.ComponentModel.DataAnnotations;

namespace DairyFarm.Pages.Model
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
