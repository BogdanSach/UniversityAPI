namespace UniversityAPI.Models.DTO.DormDTOs
{
    public class DormDto
    {
        public Guid Id { get; set; } //PK
        public string Type { get; set; }
        public int Capacity { get; set; }

        // Navigation properties
        public LocationDto LocationDto { get; set; }
        public Guid UniversityId { get; set; }
    }
}
