using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDBApi.Models;

// [BsonIgnoreExtraElements]
public class PhoneInfo

{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("name")] 
    public string Name { get; set; } = null!;

    [BsonElement("email")]
    public string Email { get; set; } = null!;
    
    [BsonElement("phoneNumber")]
    public string PhoneNumber { get; set; } = null!;

    
    // [BsonElement("items")]
    // [JsonPropertyName("items")]
    // public Array Products { get; set; } = null!;

}