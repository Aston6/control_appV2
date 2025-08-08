using Microsoft.EntityFrameworkCore;
using MyApp2.Models;

namespace MyApp2.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users => Set<User>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                        .HasIndex(u => u.Username)
                        .IsUnique(); // enforce unique usernames
        }
    }
}
