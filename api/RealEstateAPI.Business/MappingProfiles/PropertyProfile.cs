using AutoMapper;
using RealEstateAPI.Core.DTOs;
using RealEstateAPI.Core.Entities;

namespace RealEstateAPI.Business.MappingProfiles
{
    public class PropertyProfile : Profile
    {
        public PropertyProfile()
        {
            CreateMap<Property, PropertyDto>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom<PropertyImageResolver>());

            CreateMap<PropertyDto, Property>()
                .ForMember(dest => dest.Images, opt => opt.Ignore())
                .ForMember(dest => dest.Traces, opt => opt.Ignore());

            CreateMap<Owner, OwnerDto>().ReverseMap();
            CreateMap<PropertyImage, PropertyDto>().ReverseMap();
            CreateMap<PropertyTrace, PropertyDto>().ReverseMap();
        }
    }

    public class PropertyImageResolver : IValueResolver<Property, PropertyDto, string?>
    {
        public string? Resolve(Property source, PropertyDto destination, string? destMember, ResolutionContext context)
        {
            if (source.Images == null || !source.Images.Any())
                return null;

            var enabledImage = source.Images.FirstOrDefault(img => img.Enabled);
            return enabledImage?.File;
        }
    }
}