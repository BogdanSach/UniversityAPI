using AutoMapper;
using UniversityAPI.Models.Domain;
using UniversityAPI.Models.DTO;
using UniversityAPI.Models.DTO.UniDTOs;
using UniversityAPI.Models.DTO.DormDTOs;

namespace UniversityAPI.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //University
            CreateMap<University, UniversityDto>()
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
                .ReverseMap();

            CreateMap<AddUniversityRequestDto, University>().ReverseMap();

            CreateMap<UpdateUniversityRequestDto, University>().ReverseMap();


            //Dorm
            CreateMap<Dorm, DormDto>()
                .ForMember(dest => dest.LocationDto, opt => opt.MapFrom(src => src.Location))
                .ReverseMap();
            CreateMap<DormType, DormTypeDto>().ReverseMap();


            //University Building
            CreateMap<UniversityBuilding, UniversityBuildingDto>()
                .ForMember(dest => dest.LocationDto, opt => opt.MapFrom(src => src.Location))
                .ReverseMap();


            //Location
            CreateMap<Location, LocationDto>().ReverseMap();
        }
    }
}
