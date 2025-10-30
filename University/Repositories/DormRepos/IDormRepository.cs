using UniversityAPI.Models.Domain;

namespace UniversityAPI.Repositories.DormRepos
{
    public interface IDormRepository
    {
        Task<IEnumerable<Dorm>> GetAllAsync();
        Task<Dorm?> GetByIdAsync( Guid id );
        Task<Dorm> CreateAsync(Dorm dorm);
        Task<Dorm?> UpdateAsync(Guid id, Dorm dorm);
        Task<Dorm?> DeleteAsync(Guid id);
    }
}
