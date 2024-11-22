using DairyFarm.Data.Entity;

namespace DairyFarm.Model
{
    public class Cattle
    {
        
    public int CowId { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Breed { get; set; }
        public bool IsMilking { get; set; }

    }
}
