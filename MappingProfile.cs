using AutoMapper;

namespace QPD_Middle.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DaDataResponse, AddressResponse>()
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
                .ForMember(dest => dest.HouseNumber, opt => opt.MapFrom(src => src.House))
                .ForMember(dest => dest.FlatNumber, opt => opt.MapFrom(src => src.Flat));
        }
    }
}
