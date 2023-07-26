using BulkyWebRazor_Temp.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyWebRazor_Temp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; } // help create database name Categories

        protected override void OnModelCreating(ModelBuilder modelBuilder) // step 9 create the data
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Id = 2, Name = "ABC", DisplayOrder = 2 },
                new Category { Id = 3, Name = "DEF", DisplayOrder = 3 }
                );
        }
    }
}
