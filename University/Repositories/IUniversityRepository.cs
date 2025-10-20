using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityAPI.Models.Domain;

namespace UniversityAPI.Repositories
{
    public interface IUniversityRepository
    {
        Task<IEnumerable<University>> GetAllAsync();
        Task<University?> GetByIdAsync(Guid id);
        Task<University> CreateAsync(University university);
        Task<University?> UpdateAsync(Guid id, University university);
        Task<University?> DeleteAsync(Guid id);
    }
}
