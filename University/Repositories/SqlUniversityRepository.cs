using Microsoft.EntityFrameworkCore;
using UniversityAPI.Data;
using UniversityAPI.Models.Domain;

namespace UniversityAPI.Repositories
{
    public class SqlUniversityRepository : IUniversityRepository
    {
        private readonly UniDbContext dbContext;

        public SqlUniversityRepository(UniDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<University> CreateAsync(University university)
        {
            await dbContext.Universities.AddAsync(university);
            await dbContext.SaveChangesAsync();
            return university;
        }

        public async Task<University?> DeleteAsync(Guid id)
        {
            var existingUniversity = await dbContext.Universities.FirstOrDefaultAsync(x => x.Id == id);
            if (existingUniversity == null)
            {
                return null;
            }

            dbContext.Universities.Remove(existingUniversity);
            await dbContext.SaveChangesAsync();
            return existingUniversity;
        }

        public async Task<IEnumerable<University>> GetAllAsync()
        {
            return await dbContext.Universities.ToListAsync();
        }

        public async Task<University?> GetByIdAsync(Guid id)
        {
            return await dbContext.Universities.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<University?> UpdateAsync(Guid id, University university)
        {
            var existingUniversity = await dbContext.Universities.FirstOrDefaultAsync(x => x.Id == id);
            if (existingUniversity == null)
            {
                return null;
            }
            existingUniversity.Name = university.Name;
            existingUniversity.Address = university.Address;
            existingUniversity.Url = university.Url;

            await dbContext.SaveChangesAsync();
            return existingUniversity;
        }
    }
}
