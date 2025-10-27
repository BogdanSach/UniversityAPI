using UniversityAPI.Models.Domain;

namespace UniversityAPI.Models.DTO
{
    public class UniversityDto
    {
        public Guid Id { get; set; }//PK
        public string Name { get; set; }
        public string Url { get; set; }
        public string? Description { get; set; }

        public LocationDto Location { get; set; }

    }
}
