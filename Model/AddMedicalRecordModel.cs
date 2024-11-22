using System.ComponentModel.DataAnnotations;

namespace DairyFarm.Model
{
    public class AddMedicalRecordClass
    {
         
        [Required]
        public int CowId { get; set; }

        [Required]
        public DateTime LastCalvingDate { get; set; }

        public DateTime? BreedingDate { get; set; }
    }
}

