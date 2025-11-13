using AutoMapper;
using UniversityAPI.Models.Domain;
using UniversityAPI.Models.DTO;
using UniversityAPI.Models.DTO.UniDTOs;
using UniversityAPI.Models.DTO.DormDTOs;
using UniversityAPI.Models.DTO.UniBuildingDto;

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
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
                .ForMember(dest => dest.UniversityName, opt => opt.MapFrom(src => src.University.Name))
                .ReverseMap();
            CreateMap<DormType, DormTypeDto>().ReverseMap();
            CreateMap<AddDormRequestDto, Dorm>().ReverseMap();
            CreateMap<UpdateDormRequestDto, Dorm>().ReverseMap();


            //University Building
            CreateMap<UniversityBuilding, UniversityBuildingDto>()
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
                .ForMember(dest => dest.UniversityName, opt => opt.MapFrom(src => src.University.Name))
                .ReverseMap();
            CreateMap<AddUniversityBuildingRequestDto, UniversityBuilding>().ReverseMap();
            CreateMap<UpdateUniversityBuildingRequestDto, UniversityBuilding>().ReverseMap();


            //Location
            CreateMap<Location, LocationDto>().ReverseMap();
        }
    }
}
