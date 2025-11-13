using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using UniversityAPI.Data;
using UniversityAPI.Models.Domain;

namespace UniversityAPI.Repositories.UniBuildingRepos
{
    public class SqlUniversityBuildingRepository : IUniversityBuildingRepository
    {
        private readonly UniDbContext dbContext;

        public SqlUniversityBuildingRepository(UniDbContext DbContext)
        {
            dbContext = DbContext;
        }
        public async Task<UniversityBuilding> CreateAsync(UniversityBuilding universityBuilding)
        {
            await dbContext.Locations.AddAsync(universityBuilding.Location);

            await dbContext.UniversityBuildings.AddAsync(universityBuilding);

            await dbContext.SaveChangesAsync();

            return universityBuilding;
        }

        public async Task<UniversityBuilding?> DeleteAsync(Guid universityId, Guid id)
        {
            var existingUniBuilding = await dbContext.UniversityBuildings.FirstOrDefaultAsync(x => x.Id == id && x.UniversityId == universityId);
            if (existingUniBuilding == null) { return null; }

            dbContext.UniversityBuildings.Remove(existingUniBuilding);
            await dbContext.SaveChangesAsync();

            return existingUniBuilding;
        }

        public async Task<IEnumerable<UniversityBuilding>> GetAllAsync()
        {
            var uniBuildings = await dbContext.UniversityBuildings
                .Include(ub =>  ub.Location)
                .ToListAsync();
            return uniBuildings;
        }

        public async Task<UniversityBuilding?> GetByIdAsync(Guid id)
        {
            var universityBuilding = await dbContext.UniversityBuildings
                .Include(ub => ub.Location)
                .FirstOrDefaultAsync(x => x.Id == id);

            return universityBuilding;
        }

        public async Task<UniversityBuilding?> GetByIdForUniAsync(Guid universityId, Guid id)
        {
            var universityBuilding = await dbContext.UniversityBuildings
                .Include(ub => ub.Location)
                .FirstOrDefaultAsync(x => x.Id == id && x.UniversityId == universityId);

            return universityBuilding;
        }

        public async Task<IEnumerable<UniversityBuilding>> GetByUniversityIdAsync(Guid universityId)
        {
            return await dbContext.UniversityBuildings
                .Where(ub => ub.UniversityId == universityId)
                .Include(ub => ub.Location)
                .ToListAsync();
        }

        public async Task<UniversityBuilding?> UpdateAsync(Guid universityId, Guid id, UniversityBuilding universityBuilding)
        {
            var existingUniBuilding = await dbContext.UniversityBuildings
                .Include(ub => ub.Location)
                .FirstOrDefaultAsync(x => x.Id == id && x.UniversityId == universityId);

            if (existingUniBuilding == null) { return null; }

            existingUniBuilding.Number = universityBuilding.Number;

            if (existingUniBuilding.Location != null && universityBuilding.Location != null)
            {
                existingUniBuilding.Location.City = universityBuilding.Location.City;
                existingUniBuilding.Location.Region = universityBuilding.Location.Region;
                existingUniBuilding.Location.Street = universityBuilding.Location.Street;
                existingUniBuilding.Location.Number = universityBuilding.Location.Number;
            }

            await dbContext.SaveChangesAsync();
            return existingUniBuilding;
        }
    }
}
