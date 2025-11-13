using UniversityAPI.Models.Domain;

namespace UniversityAPI.Models.DTO.UniBuildingDto
{
    public class UniversityBuildingDto
    {
        public Guid Id { get; set; }
        public int Number { get; set; }

        public LocationDto Location { get; set; }
        // Navigation properties
        public Guid UniversityId { get; set; }
        public string UniversityName { get; set; }
    }
}
