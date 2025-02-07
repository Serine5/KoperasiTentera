using KoperasiTenteraDAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace KoperasiTenteraDAL.Context
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(t => t.Id);
            modelBuilder.Entity<User>().Property(t => t.Name);
            modelBuilder.Entity<User>().Property(t => t.ICNumber);
            modelBuilder.Entity<User>().Property(t => t.PhoneNumber);
            modelBuilder.Entity<User>().Property(t => t.Email);
            modelBuilder.Entity<User>().Property(t => t.Pin);
            modelBuilder.Entity<User>().Property(t => t.PrivacyPolicyEnabled);
            modelBuilder.Entity<User>().Property(t => t.BiometricEnabled);
        }
    }


}
