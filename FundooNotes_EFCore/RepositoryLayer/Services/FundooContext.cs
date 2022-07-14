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
    }
}
