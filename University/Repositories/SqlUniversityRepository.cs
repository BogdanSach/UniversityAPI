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
            var universities = await dbContext.Universities
                .Include(u => u.Location)
                .ToListAsync();
            return universities;
        }

        public async Task<University?> GetByIdAsync(Guid id)
        {
            var university = await dbContext.Universities
                .Include(u => u.Location)
                .FirstOrDefaultAsync(x => x.Id == id);
            return university;
        }

        public async Task<University?> UpdateAsync(Guid id, University university)
        {
            var existingUniversity = await dbContext.Universities.Include(u => u.Location).FirstOrDefaultAsync(x => x.Id == id);
            if (existingUniversity == null)
            {
                return null;
            }
            existingUniversity.Name = university.Name;
            existingUniversity.Url = university.Url;
            existingUniversity.Description = university.Description;

            if (existingUniversity.Location != null && university.Location != null)
            {
                existingUniversity.Location.City = university.Location.City;
                existingUniversity.Location.Region = university.Location.Region;
                existingUniversity.Location.Street = university.Location.Street;
                existingUniversity.Location.Number = university.Location.Number;
            }

            await dbContext.SaveChangesAsync();
            return existingUniversity;
        }
    }
}
