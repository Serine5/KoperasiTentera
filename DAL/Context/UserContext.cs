using KoperasiTenteraDAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace KoperasiTenteraDAL.Context
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => x.Id);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                 @"Server=192.168.40.4;Database=SerineDatabase;Trusted_Connection=True;");
        }
    }
}
