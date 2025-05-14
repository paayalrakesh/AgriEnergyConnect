using Microsoft.EntityFrameworkCore;
using AgriEnergyConnect.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace AgriEnergyConnect.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<FarmerProfile> FarmerProfiles { get; set; }


        // Later: Add DbSet<Product> and DbSet<FarmerProfile>

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = 1, RoleName = "Farmer" },
                new Role { RoleId = 2, RoleName = "Employee" }
            );
        }
    }
}
