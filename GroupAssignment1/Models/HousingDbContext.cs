using Microsoft.EntityFrameworkCore;

namespace GroupAssignment1.Models
{
    public class HousingDbContext : DbContext
    {
        public HousingDbContext(DbContextOptions<HousingDbContext> options) : base(options) {
           Database.EnsureCreated();
        }
        public DbSet<Housing> Housings { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Housing>()
                .HasOne(h => h.Owner)
                .WithMany(c => c.OwnedHousings)
                .HasForeignKey(h => h.OwnerId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
