using Microsoft.EntityFrameworkCore;
using DotaDashboard.Models;


namespace DotaDashboard.Data
{
    public class ApiContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public ApiContext (DbContextOptions<ApiContext> options): base (options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(u => new { u.Username, u.SteamId });
        }

    }
}