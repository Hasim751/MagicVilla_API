using MagicVilla_VillaAAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Villa> Villas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa()
                {
                    Id = 1,
                    Name = "Goa Villa",
                    Description = "Goal best villa",
                    Amenity = "",
                    Occupancy = 5,
                    Rate = 5,
                    Sqft = 550,
                    ImageUrl = "https://cdn.lecollectionist.com/lc/production/uploads/photos/house-1910/2018-04-30-214088d759242733859024dd8690041f.jpg?q=65",
                    Created = DateTime.Now,

                }
                );
        }
    }
}
