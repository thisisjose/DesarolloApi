using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Drivers.Api.Models;

public class Drive
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id {get;set;}= string.Empty;

    [BsonElement("Name")]

    public string Name{get;set;}= string.Empty;
    public int Number {get;set;}
    public string Team {get;set;}=string.Empty;
}