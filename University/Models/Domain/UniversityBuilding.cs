namespace UniversityAPI.Models.Domain
{
    public class UniversityBuilding
    {
        public Guid Id { get; set; }    //PK
        public int Number { get; set; }

        // FK to location
        public Guid LocationId { get; set; }    //FK
        public Location Location { get; set; }
        // Navigation properties
        public Guid UniversityId { get; set; }  //FK
        public University University { get; set; }
    }
}
