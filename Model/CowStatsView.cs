namespace DairyFarm.Model
{
    public class CowStatsView
    {
        public int Cowid { get; set; }

        public string CowName { get; set; }

        public string Breed { get; set; }

        public DateTime PurchaseDate { get; set; }

        public DateTime BreedingDate { get; set; }

        public DateTime LactationEndDate { get; set; }

        public DateTime LastCalvingDate { get; set; }

        public DateTime NextCalvingDate { get; set; }
    }
}
