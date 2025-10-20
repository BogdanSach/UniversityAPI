using System.ComponentModel.DataAnnotations;

namespace UniversityAPI.Models.DTO.UniDTOs
{
    public class updateUniversityRequestDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Url { get; set; }
        public string? Description { get; set; }
    }
}
