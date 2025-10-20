using AutoMapper;
using UniversityAPI.Models.Domain;
using UniversityAPI.Models.DTO;

namespace UniversityAPI.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<University, UniversityDto>().ReverseMap();
            
        }
    }
}
