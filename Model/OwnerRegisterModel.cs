using System.ComponentModel.DataAnnotations;

namespace DairyFarm.Model
{
    public class OwnerRegisterClass
    {
        [Required]
        public string OwnerName { get; set; }

        [Required, EmailAddress]
        public string OwnerEmail { get; set; }

        [Required]
        public string OwnerPassword { get; set; }

        [Required]
        public string OwnerPhone { get; set; }


    }
}
