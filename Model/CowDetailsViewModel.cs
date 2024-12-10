namespace DairyFarm.Model
{
    public class CowDetailsViewModel
    {
        public string Name { get; set; }
        public string Breed { get; set; }
        public DateTime DOB { get; set; }
        public string HealthStatus { get; set; }

        public decimal FeedExpense { get; set; }

        public decimal TotalMilkProduced { get; set; }

        public decimal MilkIncome { get; set; }

        public decimal Profit { get; set; }

        public decimal MedicalExpense { get; set; }

    }

}
