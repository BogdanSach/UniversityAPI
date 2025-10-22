using AutoMapper;
using UniversityAPI.Models.Domain;
using UniversityAPI.Models.DTO;
using UniversityAPI.Models.DTO.UniDTOs;

namespace UniversityAPI.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<University, UniversityDto>().ReverseMap();
            CreateMap<AddUniversityRequestDto, University>().ReverseMap();
            CreateMap<UpdateUniversityRequestDto, University>().ReverseMap();


        }
    }
}
