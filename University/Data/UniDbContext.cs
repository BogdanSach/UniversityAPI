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
    }
}

