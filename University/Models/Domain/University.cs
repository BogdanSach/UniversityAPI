namespace UniversityAPI.Models.Domain
{
    public class University
    {
        public Guid Id { get; set; }//PK
        public string Name { get; set; }
        public string Url { get; set; }
        public string? Description { get; set; }

        // FK to location
        public Guid LocationId { get; set; } //FK
        public Location Location { get; set; }

        // Navigation properties
        public ICollection<Dorm> Dorms { get; set; }
        public ICollection<UniversityBuilding> UniversityBuildings { get; set; }
    }
}
