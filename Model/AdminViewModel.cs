using DairyFarm.Data.Entity;

namespace DairyFarm.Model
{
    public class AdminViewModel
    {
        public string OwnerName { get; set; }
        public int CowCount { get; set; }
        public string SubscriptionTier { get; set; }
    }
}
