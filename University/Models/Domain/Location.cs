namespace UniversityAPI.Models.Domain
{
    public class Location
    {
        public Guid Id { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Street { get; set; }
        public int Number {  get; set; }

        // Navigation properties
        public University University { get; set; }
        public Dorm Dorm { get; set; }
        public UniversityBuilding UniversityBuilding { get; set; }
    }
}
