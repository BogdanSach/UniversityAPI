using Microsoft.EntityFrameworkCore;
using UniversityAPI.Models.Domain;

namespace UniversityAPI.Data
{
    public class UniDbContext : DbContext
    {
        public UniDbContext(DbContextOptions<UniDbContext> options) : base(options)
        {   
        }

        public DbSet<University> Universities { get; set; }
        public DbSet<Dorm> Dorms { get; set; }
        public DbSet<DormType> DormTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ---One-to-One---
            // University - location
            modelBuilder.Entity<University>()
                .HasOne(u => u.Location)
                .WithOne()
                .HasForeignKey<University>(u => u.LocationId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Dorm - location
            modelBuilder.Entity<Dorm>()
                .HasOne(d => d.Location)
                .WithOne()
                .HasForeignKey<Dorm>(d => d.LocationId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // UniversityBuilding - location
            modelBuilder.Entity<UniversityBuilding>()
                .HasOne(ub => ub.Location)
                .WithOne()
                .HasForeignKey<UniversityBuilding>(ub => ub.LocationId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // ---One-to-Many---
            // University - Dorms
            modelBuilder.Entity<Dorm>()
                .HasOne(d => d.University)
                .WithMany(u => u.Dorms)
                .HasForeignKey(d => d.UniversityId)
                .IsRequired();

            // University - UniversityBuildings
            modelBuilder.Entity<UniversityBuilding>()
                .HasOne(ub => ub.University)
                .WithMany(u => u.UniversityBuildings)
                .HasForeignKey(ub => ub.UniversityId)
                .IsRequired();

            // ---Many-to-One---
            // Dorms - DormTypes
            modelBuilder.Entity<Dorm>()
                .HasOne(d => d.DormType)
                .WithMany(dt => dt.Dorms)
                .HasForeignKey(d => d.DormtypeId)
                .IsRequired();

            // Seed Data for DormTypes
            var dormTypes = new List<DormType>()
            {
                new DormType() { Id = Guid.Parse("8d7ed0fe-900f-4780-80e6-12fd2f0ec4a4"), TypeName = "Corridor" },
                new DormType() { Id = Guid.Parse("c0281486-d9ec-4bf8-8164-3cce39241d0a"), TypeName = "Block" },
            };

            modelBuilder.Entity<DormType>().HasData(dormTypes);
        }
    }
}

