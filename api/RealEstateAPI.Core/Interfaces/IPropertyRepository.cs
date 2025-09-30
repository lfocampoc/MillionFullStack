using RealEstateAPI.Core.DTOs;
using RealEstateAPI.Core.Entities;

namespace RealEstateAPI.Core.Interfaces
{
    public interface IPropertyRepository
    {
        Task<IEnumerable<Property>> GetAllAsync();
        Task<Property?> GetByIdAsync(string id);
        Task<IEnumerable<Property>> GetFilteredAsync(PropertyFilterDto filter);
        Task<Property> CreateAsync(Property property);
        Task<Property?> UpdateAsync(string id, Property property);
        Task<bool> DeleteAsync(string id);
    }
}
