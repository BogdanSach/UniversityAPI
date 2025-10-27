using UniversityAPI.Models.Domain;

namespace UniversityAPI.Models.DTO.DormDTOs
{
    public class DormTypeDto
    {
        public Guid Id { get; set; }
        public string TypeName { get; set; }

        public ICollection<Dorm> Dorms { get; set; }
    }
}
