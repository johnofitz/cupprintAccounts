using Microsoft.EntityFrameworkCore;


namespace cupprintAccounts.Context
{
    public partial class DbContextFinance : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("Data Source=IEENN-MARS\\SAGE200;Initial Catalog=Thardata;User ID=tharUser;Password=tharUSER;Encrypt=False");
        }

        public DbSet<UpsRateCard> UpsRateCard { get; set; }

        public DbSet<TharsternDeliveries> TharstenDeliveries { get; set; }
    }
}
