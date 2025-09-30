using Microsoft.AspNetCore.Mvc;
using RealEstateAPI.Core.DTOs;
using RealEstateAPI.Core.Interfaces;

namespace RealEstateAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertiesController : ControllerBase
    {
        private readonly IPropertyService _propertyService;
        private readonly ILogger<PropertiesController> _logger;

        public PropertiesController(IPropertyService propertyService, ILogger<PropertiesController> logger)
        {
            _propertyService = propertyService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<PropertyDto>>>> GetProperties([FromQuery] PropertyFilterDto filter)
        {
            try
            {
                _logger.LogInformation("Obteniendo propiedades con filtros: {Filter}", filter);
                var properties = await _propertyService.GetFilteredPropertiesAsync(filter);
                _logger.LogInformation("Se obtuvieron {Count} propiedades", properties.Count());
                return Ok(ApiResponse<IEnumerable<PropertyDto>>.SuccessResult(properties, "Propiedades obtenidas exitosamente"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener propiedades con filtros: {Filter}", filter);
                return Ok(ApiResponse<IEnumerable<PropertyDto>>.ErrorResult(ex.Message, "Error al obtener propiedades"));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<PropertyDto>>> GetProperty(string id)
        {
            try
            {
                var property = await _propertyService.GetPropertyByIdAsync(id);
                if (property == null)
                {
                    return Ok(ApiResponse<PropertyDto>.ErrorResult("Propiedad no encontrada", "No se encontró la propiedad solicitada"));
                }
                return Ok(ApiResponse<PropertyDto>.SuccessResult(property, "Propiedad obtenida exitosamente"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la propiedad {PropertyId}", id);
                return Ok(ApiResponse<PropertyDto>.ErrorResult(ex.Message, "Error al obtener la propiedad"));
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<PropertyDto>>> CreateProperty([FromBody] PropertyDto propertyDto)
        {
            try
            {
                _logger.LogInformation("Creando nueva propiedad: {PropertyName} para propietario {OwnerId}", 
                    propertyDto.Name, propertyDto.IdOwner);
                var createdProperty = await _propertyService.CreatePropertyAsync(propertyDto);
                _logger.LogInformation("Propiedad creada exitosamente con ID: {PropertyId}", createdProperty.Id);
                return Ok(ApiResponse<PropertyDto>.SuccessResult(createdProperty, "Propiedad creada exitosamente"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear la propiedad: {PropertyName}", propertyDto.Name);
                return Ok(ApiResponse<PropertyDto>.ErrorResult(ex.Message, "Error al crear la propiedad"));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<PropertyDto>>> UpdateProperty(string id, [FromBody] PropertyDto propertyDto)
        {
            try
            {
                var updatedProperty = await _propertyService.UpdatePropertyAsync(id, propertyDto);
                if (updatedProperty == null)
                {
                    return Ok(ApiResponse<PropertyDto>.ErrorResult("Propiedad no encontrada", "No se encontró la propiedad para actualizar"));
                }
                return Ok(ApiResponse<PropertyDto>.SuccessResult(updatedProperty, "Propiedad actualizada exitosamente"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la propiedad {PropertyId}", id);
                return Ok(ApiResponse<PropertyDto>.ErrorResult(ex.Message, "Error al actualizar la propiedad"));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteProperty(string id)
        {
            try
            {
                var result = await _propertyService.DeletePropertyAsync(id);
                if (!result)
                {
                    return Ok(ApiResponse<bool>.ErrorResult("Propiedad no encontrada", "No se encontró la propiedad para eliminar"));
                }
                return Ok(ApiResponse<bool>.SuccessResult(true, "Propiedad eliminada exitosamente"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la propiedad {PropertyId}", id);
                return Ok(ApiResponse<bool>.ErrorResult(ex.Message, "Error al eliminar la propiedad"));
            }
        }
    }
}
