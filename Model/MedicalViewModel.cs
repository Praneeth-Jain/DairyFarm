using System.ComponentModel.DataAnnotations;

namespace DairyFarm.Model
{
    public class MedicalViewModel
    {

        public string Cowname { get; set; }

        public DateTime DOB {  get; set; }

        public DateTime LastCalvingDate { get; set; }


        public DateTime BreedingDate { get; set; }


        public DateTime LactationEndDate { get; set; }

        public DateTime DryDaysStart { get; set; }

        public DateTime DryDaysEnd { get; set; }

        public DateTime NextCalvingDate { get; set; }

    }
}
