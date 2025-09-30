using RealEstateAPI.Core.DTOs;

namespace RealEstateAPI.Core.Interfaces
{
    public interface IPropertyService
    {
        Task<IEnumerable<PropertyDto>> GetAllPropertiesAsync();
        Task<PropertyDto?> GetPropertyByIdAsync(string id);
        Task<IEnumerable<PropertyDto>> GetFilteredPropertiesAsync(PropertyFilterDto filter);
        Task<PropertyDto> CreatePropertyAsync(PropertyDto propertyDto);
        Task<PropertyDto?> UpdatePropertyAsync(string id, PropertyDto propertyDto);
        Task<bool> DeletePropertyAsync(string id);
    }
}
