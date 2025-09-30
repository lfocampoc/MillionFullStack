using MongoDB.Driver;
using RealEstateAPI.Core.DTOs;
using RealEstateAPI.Core.Entities;
using RealEstateAPI.Core.Interfaces;

namespace RealEstateAPI.Repository
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly IMongoCollection<Property> _properties;
        private readonly IMongoCollection<Owner> _owners;

        public PropertyRepository(IMongoDatabase database)
        {
            _properties = database.GetCollection<Property>("properties");
            _owners = database.GetCollection<Owner>("owners");
        }

        public async Task<IEnumerable<Property>> GetAllAsync()
        {
            return await _properties.Find(_ => true).ToListAsync();
        }

        public async Task<Property?> GetByIdAsync(string id)
        {
            return await _properties.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Property>> GetFilteredAsync(PropertyFilterDto filter)
        {
            var filterBuilder = Builders<Property>.Filter;
            var filters = new List<FilterDefinition<Property>>();

            // Búsqueda de texto: buscar en name Y address simultáneamente
            if (!string.IsNullOrEmpty(filter.Name))
            {
                var textSearchFilter = filterBuilder.Or(
                    filterBuilder.Regex(p => p.Name, new MongoDB.Bson.BsonRegularExpression(filter.Name, "i")),
                    filterBuilder.Regex(p => p.Address, new MongoDB.Bson.BsonRegularExpression(filter.Name, "i"))
                );
                filters.Add(textSearchFilter);
            }

            // Filtro específico por dirección (si se proporciona por separado)
            if (!string.IsNullOrEmpty(filter.Address) && string.IsNullOrEmpty(filter.Name))
            {
                filters.Add(filterBuilder.Regex(p => p.Address, new MongoDB.Bson.BsonRegularExpression(filter.Address, "i")));
            }

            if (filter.MinPrice.HasValue)
            {
                filters.Add(filterBuilder.Gte(p => p.Price, filter.MinPrice.Value));
            }

            if (filter.MaxPrice.HasValue)
            {
                filters.Add(filterBuilder.Lte(p => p.Price, filter.MaxPrice.Value));
            }

            var combinedFilter = filters.Any() ? filterBuilder.And(filters) : filterBuilder.Empty;

            var query = _properties.Find(combinedFilter);

            if (filter.Page > 0 && filter.PageSize > 0)
            {
                query = query.Skip((filter.Page - 1) * filter.PageSize).Limit(filter.PageSize);
            }

            return await query.ToListAsync();
        }

        public async Task<Property> CreateAsync(Property property)
        {
            await _properties.InsertOneAsync(property);
            return property;
        }

        public async Task<Property?> UpdateAsync(string id, Property property)
        {
            var result = await _properties.ReplaceOneAsync(p => p.Id == id, property);
            return result.IsAcknowledged ? property : null;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _properties.DeleteOneAsync(p => p.Id == id);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }
    }
}
