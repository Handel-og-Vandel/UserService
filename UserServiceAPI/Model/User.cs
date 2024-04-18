using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class User
{
    [BsonId]
    public Guid Id { get; set; } // MongoDB ObjectId as the primary key

    [BsonElement("givenName")]
    [BsonRepresentation(BsonType.String)]
    public string GivenName { get; set; }

    [BsonElement("familyName")]
    [BsonRepresentation(BsonType.String)]
    public string FamilyName { get; set; }

    [BsonElement("department")]
    [BsonRepresentation(BsonType.String)]
    public string Department { get; set; }

    [BsonElement("email")]
    [BsonRepresentation(BsonType.String)]
    public string Email { get; set; }

    [BsonElement("loginIdentifier")]
    [BsonRepresentation(BsonType.String)]
    public string LoginIdentifier { get; set; }

    [BsonElement("password")]
    [BsonRepresentation(BsonType.String)]
    public string Password { get; set; }

    [BsonElement("salt")]
    [BsonRepresentation(BsonType.String)]
    public string Salt { get; set; }

}
