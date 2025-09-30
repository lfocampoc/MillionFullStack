using AutoMapper;
using RealEstateAPI.Core.DTOs;
using RealEstateAPI.Core.Entities;
using RealEstateAPI.Core.Interfaces;

namespace RealEstateAPI.Business
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;

        public PropertyService(IPropertyRepository propertyRepository, IMapper mapper)
        {
            _propertyRepository = propertyRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PropertyDto>> GetAllPropertiesAsync()
        {
            var properties = await _propertyRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PropertyDto>>(properties);
        }

        public async Task<PropertyDto?> GetPropertyByIdAsync(string id)
        {
            var property = await _propertyRepository.GetByIdAsync(id);
            return property != null ? _mapper.Map<PropertyDto>(property) : null;
        }

        public async Task<IEnumerable<PropertyDto>> GetFilteredPropertiesAsync(PropertyFilterDto filter)
        {
            var properties = await _propertyRepository.GetFilteredAsync(filter);
            return _mapper.Map<IEnumerable<PropertyDto>>(properties);
        }

        public async Task<PropertyDto> CreatePropertyAsync(PropertyDto propertyDto)
        {
            var property = _mapper.Map<Property>(propertyDto);
            var createdProperty = await _propertyRepository.CreateAsync(property);
            return _mapper.Map<PropertyDto>(createdProperty);
        }

        public async Task<PropertyDto?> UpdatePropertyAsync(string id, PropertyDto propertyDto)
        {
            var existingProperty = await _propertyRepository.GetByIdAsync(id);
            if (existingProperty == null)
                return null;

            _mapper.Map(propertyDto, existingProperty);
            existingProperty.Id = id; // Asegurar que el ID no cambie

            var updatedProperty = await _propertyRepository.UpdateAsync(id, existingProperty);
            return updatedProperty != null ? _mapper.Map<PropertyDto>(updatedProperty) : null;
        }

        public async Task<bool> DeletePropertyAsync(string id)
        {
            return await _propertyRepository.DeleteAsync(id);
        }

    }
}
