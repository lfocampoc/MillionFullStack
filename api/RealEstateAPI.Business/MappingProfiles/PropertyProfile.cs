using AutoMapper;
using RealEstateAPI.Core.DTOs;
using RealEstateAPI.Core.Entities;

namespace RealEstateAPI.Business.MappingProfiles
{
    public class PropertyProfile : Profile
    {
        public PropertyProfile()
        {
            // Mapeo de Property a PropertyDto - uso un resolver personalizado para la imagen
            CreateMap<Property, PropertyDto>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom<PropertyImageResolver>());

            // Mapeo de PropertyDto a Property - ignoro las colecciones ya que no las necesito en el DTO
            CreateMap<PropertyDto, Property>()
                .ForMember(dest => dest.Images, opt => opt.Ignore())
                .ForMember(dest => dest.Traces, opt => opt.Ignore());

            // Mapeos bidireccionales para las entidades relacionadas
            CreateMap<Owner, OwnerDto>().ReverseMap();
            CreateMap<PropertyImage, PropertyDto>().ReverseMap();
            CreateMap<PropertyTrace, PropertyDto>().ReverseMap();
        }
    }

    public class PropertyImageResolver : IValueResolver<Property, PropertyDto, string?>
    {
        public string? Resolve(Property source, PropertyDto destination, string? destMember, ResolutionContext context)
        {
            // Si no hay imágenes, retorno null
            if (source.Images == null || !source.Images.Any())
                return null;

            // Busco la primera imagen habilitada para usar como imagen principal
            var enabledImage = source.Images.FirstOrDefault(img => img.Enabled);
            return enabledImage?.File;
        }
    }
}