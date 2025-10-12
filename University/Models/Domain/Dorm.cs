namespace UniversityAPI.Models.Domain
{
    public class Dorm
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public int Capacity { get; set; }
        


        // Navigation properties
        public Guid UniversityId { get; set; }
        public Guid AddressId { get; set; }
    }
}
