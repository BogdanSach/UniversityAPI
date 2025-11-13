using UniversityAPI.Models.Domain;


namespace UniversityAPI.Repositories.UniBuildingRepos
{
    public interface IUniversityBuildingRepository
    {
        Task<IEnumerable<UniversityBuilding>> GetAllAsync();
        Task<UniversityBuilding?> GetByIdAsync(Guid id);
        Task<IEnumerable<UniversityBuilding>> GetByUniversityIdAsync(Guid universityId);
        Task<UniversityBuilding?> GetByIdForUniAsync(Guid universityId, Guid id);
        Task<UniversityBuilding> CreateAsync(UniversityBuilding universityBuilding);
        Task<UniversityBuilding?> UpdateAsync(Guid universityId, Guid id, UniversityBuilding universityBuilding);
        Task<UniversityBuilding?> DeleteAsync(Guid universityId, Guid id);

    }
}
