using Microsoft.EntityFrameworkCore;

namespace GroupAssignment1.Models
{
    public class HousingDbContext : DbContext
    {
        public HousingDbContext(DbContextOptions<HousingDbContext> options) : base(options) {
            Database.EnsureCreated();
        }
        public DbSet<Housing> Housing { get; set; }
    }
}
