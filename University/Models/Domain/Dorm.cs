namespace UniversityAPI.Models.Domain
{
    public class Dorm
    {
        public Guid Id { get; set; } //PK
        public decimal PriceOfLiving { get; set; }
        public int Capacity { get; set; }

        public Guid LocationId { get; set; }//FK
        public Location Location { get; set; }

        // Navigation properties
        public Guid UniversityId { get; set; }//FK
        public University University { get; set; }
        public Guid DormtypeId { get; set; }//FK
        public DormType DormType { get; set; }

    }
}
