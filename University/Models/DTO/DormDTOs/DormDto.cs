using UniversityAPI.Models.Domain;

namespace UniversityAPI.Models.DTO.DormDTOs
{
    public class DormDto
    {
        public Guid Id { get; set; } //PK
        public int Number { get; set; }
        public decimal PriceOfLiving { get; set; }
        public int Capacity { get; set; }

        // Navigation properties
        public LocationDto Location { get; set; }
        public Guid UniversityId { get; set; }
        public string UniversityName { get; set; }
        public DormTypeDto DormType { get; set; }
    }
}
