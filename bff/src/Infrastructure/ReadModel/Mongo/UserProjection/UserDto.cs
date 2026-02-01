namespace ProjectFollowUp.BFF.Infrastructure.ReadModel.Mongo.UserProjection;

using MongoDB.Bson.Serialization.Attributes;

public sealed class UserDto
{
    [BsonId]
    public Guid Id { get; set; }

    public Guid CredentialsId { get; set; }

    public string Email { get; set; } = default!;

    public string DisplayName { get; set; } = default!;

    public DateTimeOffset CreatedAt { get; set; }
}
