namespace UniversityAPI.Models.DTO.DormDTOs
{
    public class AddDormRequestDto
    {
        public int Number { get; set; }
        public decimal PriceOfLiving { get; set; }
        public int Capacity { get; set; }

        // Navigation properties
        public LocationDto Location { get; set; }
        public Guid DormTypeId { get; set; }

    }
}
