using Authentication.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Data
{
    public class AuthenticationDBContext : DbContext
    {
        public AuthenticationDBContext(DbContextOptions<AuthenticationDBContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Difficulty> Difficulties { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Walk> Walks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var easyId = new Guid("11111111-1111-1111-1111-111111111111");
            var moderateId = new Guid("22222222-2222-2222-2222-222222222222");
            var hardId = new Guid("33333333-3333-3333-3333-333333333333");

            var region1Id = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            var region2Id = new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");

            modelBuilder.Entity<Difficulty>().HasData(
                new Difficulty { Id = easyId, Name = "Easy" },
                new Difficulty { Id = moderateId, Name = "Moderate" },
                new Difficulty { Id = hardId, Name = "Hard" }
            );


        }
    }
}

       //     modelBuilder.Entity<Region>().HasData(
       //new Region
       //{
       //    Id = Guid.NewGuid(),
       //    Code = "EU",
       //    Name = "Europe",
       //    RegionImageUrl = "https://example.com/europe.jpg"
       //},
       //new Region
       //{
       //    Id = Guid.NewGuid(),
       //    Code = "NA",
       //    Name = "North America",
       //    RegionImageUrl = "https://example.com/na.jpg"
       //});