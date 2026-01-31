namespace ProjectFollowUp.BFF.Infrastructure.ProjectionWriters.Mongo.UserProjection;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public sealed class UserDto
{
    [BsonId]
    public ObjectId BsonId { get; set; } = default;

    public Guid Id { get; set; }

    public Guid CredentialsId { get; set; }

    public string Email { get; set; } = default!;

    public string DisplayName { get; set; } = default!;

    public DateTimeOffset CreatedAt { get; set; }
}
