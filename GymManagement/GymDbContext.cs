using Microsoft.EntityFrameworkCore;
using GymManagement.Models;
namespace GymManagementx
{
    public class GymDbContext : DbContext
    {
        public GymDbContext()
        { }

        public GymDbContext(DbContextOptions<GymDbContext> options) :
     base(options)
        { }

        public DbSet<Member_Subscriptions> Member_Subscriptions { get; set; }
        public DbSet<Members> Members { get; set; }
        public DbSet<Subscriptions> Subscriptions { get; set; }
        public DbSet<Discounts> Discounts { get; set; }
        public DbSet<Discounted_Member_Subscriptions> Discounted_Member_Subscriptions { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
