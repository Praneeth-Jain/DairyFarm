using System.ComponentModel.DataAnnotations;

namespace DairyFarm.Data.Entity
{
    public class CowBreeding
    {
        [Key]
        public int Id { get; set; }


        [Required]
        public int CowId { get; set; }

        [Required]
        public DateTime LastCalvingDate { get; set; }

       
        public DateTime BreedingDate { get; set; }

       
        public DateTime LactationEndDate { get; set; }

        public DateTime DryDaysStart { get; set; }

        public DateTime DryDaysEnd { get; set; }

        public DateTime NextCalvingDate { get; set; }

        public DateTime ModifiedAt { get; set; }= DateTime.Now;


    }
}
