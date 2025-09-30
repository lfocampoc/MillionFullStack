using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RealEstateAPI.Core.Entities
{
    public class PropertyImage
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("file")]
        public string File { get; set; } = string.Empty;

        [BsonElement("enabled")]
        public bool Enabled { get; set; } = true;

        [BsonElement("idProperty")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string IdProperty { get; set; } = string.Empty;
    }
}
