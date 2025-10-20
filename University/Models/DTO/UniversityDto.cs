using UniversityAPI.Models.Domain;

namespace UniversityAPI.Models.DTO
{
    public class UniversityDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Url { get; set; }
        public string? Description { get; set; }

        // Navigation properties
        //public ICollection<Dorm> Dorms { get; set; }
    }
}
