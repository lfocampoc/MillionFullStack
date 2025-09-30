using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RealEstateAPI.Core.Entities
{
    public class Property
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("name")]
        public string Name { get; set; } = string.Empty;

        [BsonElement("address")]
        public string Address { get; set; } = string.Empty;

        [BsonElement("price")]
        public decimal Price { get; set; }

        [BsonElement("codeInternal")]
        public string CodeInternal { get; set; } = string.Empty;

        [BsonElement("year")]
        public int Year { get; set; }

        [BsonElement("idOwner")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string IdOwner { get; set; } = string.Empty;

        [BsonElement("images")]
        public List<PropertyImage> Images { get; set; } = new List<PropertyImage>();

        [BsonElement("traces")]
        public List<PropertyTrace> Traces { get; set; } = new List<PropertyTrace>();
    }
}
