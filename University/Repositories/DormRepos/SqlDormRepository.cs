using Microsoft.EntityFrameworkCore;
using UniversityAPI.Data;
using UniversityAPI.Models.Domain;

namespace UniversityAPI.Repositories.DormRepos
{
    public class SqlDormRepository : IDormRepository
    {
        private readonly UniDbContext dbContext;

        public SqlDormRepository(UniDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Dorm> CreateAsync(Dorm dorm)
        {
            await dbContext.Dorms.AddAsync(dorm);
            await dbContext.SaveChangesAsync();

            return dorm;
        }

        public async Task<Dorm?> DeleteAsync(Guid id)
        {
            var existingDorm = await dbContext.Dorms.FirstOrDefaultAsync(x => x.Id == id);
            if (existingDorm != null) { return null; }

            dbContext.Dorms.Remove(existingDorm);
            await dbContext.SaveChangesAsync();
            return existingDorm;
        }

        public async Task<IEnumerable<Dorm>> GetAllAsync()
        {
            var dorms = await dbContext.Dorms
                .Include(d => d.Location)
                .ToListAsync();
            return dorms;
        }

        public async Task<Dorm?> GetByIdAsync(Guid id)
        {
            var dorm = await dbContext.Dorms
                .Include (d => d.Location)
                .FirstOrDefaultAsync(x =>x.Id == id);

            return dorm;
        }
        
        public async Task<IEnumerable<Dorm>> GetByUniversityIdAsync(Guid universityId)
        {
            return await dbContext.Dorms
                .Where(d => d.UniversityId == universityId)
                .Include(d => d.Location)
                .Include(d => d.DormType)
                .ToListAsync();
        }


        public async Task<Dorm?> UpdateAsync(Guid id, Dorm dorm)
        {
            var existingDorm = await dbContext.Dorms
                .Include(d => d.Location)
                .FirstOrDefaultAsync(x =>x.Id == id);

            if (existingDorm == null) { return null; }

            existingDorm.PriceOfLiving = dorm.PriceOfLiving;
            existingDorm.Capacity = dorm.Capacity;
            existingDorm.DormTypeId = dorm.DormTypeId;

            if (existingDorm.Location != null && dorm.Location != null)
            {
                existingDorm.Location.City = dorm.Location.City;
                existingDorm.Location.Region = dorm.Location.Region;
                existingDorm.Location.Street = dorm.Location.Street;
                existingDorm.Location.Number = dorm.Location.Number;
            }

            await dbContext.SaveChangesAsync();
            return existingDorm;
        }
    }
}
