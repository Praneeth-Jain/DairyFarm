namespace DairyFarm.Model
{
    public class MilkProductionModel
    {
        public int MilkProductionId { get; set; } 
        public int CowId { get; set; } 
        public DateTime ProductionDate { get; set; } = DateTime.Now;
        public decimal Quantity { get; set; } 
        public string Notes { get; set; }
    }

}
