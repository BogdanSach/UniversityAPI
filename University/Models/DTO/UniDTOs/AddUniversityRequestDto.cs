using System.ComponentModel.DataAnnotations;

namespace UniversityAPI.Models.DTO.UniDTOs
{
    public class AddUniversityRequestDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Url { get; set; }
        public string? Description { get; set; }
        [Required]
        public LocationDto Location { get; set; }
    }
}
