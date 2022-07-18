using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Services.Entities;

namespace RepositoryLayer.Services
{
    public class FundooContext : DbContext
    {

        public FundooContext(DbContextOptions option) : base(option)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
             .HasIndex(u => u.Email)
             .IsUnique();
        }
    }
}
