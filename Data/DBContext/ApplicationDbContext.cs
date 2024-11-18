using DairyFarm.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace DairyFarm.Data.DBContext
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions options):base(options) { }
        
        public DbSet<ByProduct> byProduct { get; set; }
        public DbSet<CowBreeding> cowBreedings { get; set; }
        public DbSet<Cows> cows { get; set; }
        public DbSet<Expense> expenses { get; set; }
        public DbSet<FeedingLog> feedinglogs { get; set; }
        public DbSet<Food> foods { get; set; }
        public DbSet<Income> incomes { get; set; }
        public DbSet<MilkProduction> milkProductions { get; set; }
        public DbSet<Notification> notifications { get; set; }

        public DbSet<Owners> owners { get; set; }

        public DbSet<Subscriptions> subscriptions { get; set; } 
    }
}
