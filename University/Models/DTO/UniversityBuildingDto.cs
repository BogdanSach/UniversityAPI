using UniversityAPI.Models.Domain;

namespace UniversityAPI.Models.DTO
{
    public class UniversityBuildingDto
    {
        public Guid Id { get; set; }
        public int Number { get; set; }

        public LocationDto LocationDto { get; set; }
        // Navigation properties
        public Guid UniversityId { get; set; }
    }
}
