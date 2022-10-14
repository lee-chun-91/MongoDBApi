using System.Globalization;
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
    public string name { get; set; } = null!;

    [BsonElement("email")]
    public string email { get; set; } = null!;
    
    [BsonElement("phoneNumber")]
    public string phoneNumber { get; set; } = null!;

    [BsonElement("date")]
    public string date { get; set; }
    
    // [BsonElement("id")]
    // public string id { get; set; }


    
    // [BsonElement("items")]
    // [JsonPropertyName("items")]
    // public Array Products { get; set; } = null!;

}