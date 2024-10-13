using HomeGardenWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeGardenWeb.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
         : base(options)
        {
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<Plants> Plants { get; set; }
        public DbSet<WateringFrequency> WateringFrequency { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Plants>()
                .Property(p => p.price)
                .HasPrecision(18, 2); 
        }
    }
}
