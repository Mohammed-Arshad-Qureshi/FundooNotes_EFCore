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

        public DbSet<Label> Label { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
             .HasIndex(u => u.Email)
             .IsUnique();

            //modelBuilder.Entity<Label>()
            //   .HasKey(p => new { p.UserId, p.NoteId });

            //modelBuilder.Entity<Label>()
            //.HasOne(s => s.user)
            //.WithMany()
            //.HasForeignKey(s => s.UserId);


            //modelBuilder.Entity<Label>()
            //.HasOne(s => s.note)
            //.WithMany()
            //.HasForeignKey(s => s.NoteId);
        }

    }
}
