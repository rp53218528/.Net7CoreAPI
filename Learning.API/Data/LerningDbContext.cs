using Learning.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Learning.API.Data
{
    public class LerningDbContext : DbContext
    {
        //constructor
        public LerningDbContext(DbContextOptions<LerningDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //seed data for Difficulties
            //Easy, Medium, Hard
            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("00d3064f-d10d-4413-a975-01260b023085"),
                    Name = "Easy"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("1778979e-939d-49b8-b286-86eb14455663"),
                    Name = "Medium"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("3652039f-0098-430b-8ed0-7235b742e527"),
                    Name = "hard"
                }
            };
            //seed difficulties to the database
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            //seed data for regions
            var regions = new List<Region>()
            {
                new Region()
                {
                    Id = Guid.Parse("2a87cb1a-33d9-469b-9731-c029fe01d622"),
                    Name = "Auckland",
                    Code = "AKL",
                    RegionImageUrl = "",

                },
                 new Region()
                {
                    Id = Guid.Parse("f4e96d60-fbd5-420f-9845-820edcf66815"),
                    Name = "Harsh",
                    Code = "HP",
                    RegionImageUrl = "",

                },
                  new Region()
                {
                    Id = Guid.Parse("8af7070f-6704-49f6-8830-c58275a124f0"),
                    Name = "TESTING",
                    Code = "TEST",
                    RegionImageUrl = "",

                },

            };
            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}
