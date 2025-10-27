using System.ComponentModel.DataAnnotations;

namespace UniversityAPI.Models.DTO.UniDTOs
{
    public class UpdateUniversityRequestDto
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
