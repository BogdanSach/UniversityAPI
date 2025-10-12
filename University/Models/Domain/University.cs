namespace UniversityAPI.Models.Domain
{
    public class University
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Url { get; set; }

        // Navigation properties
        public ICollection<Dorm> Dorms { get; set; }
    }
}
