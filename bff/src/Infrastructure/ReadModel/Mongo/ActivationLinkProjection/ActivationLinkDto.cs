namespace ProjectFollowUp.BFF.Infrastructure.ReadModel.Mongo.ActivationLinkProjection;

using MongoDB.Bson.Serialization.Attributes;

public sealed class ActivationLinkDto
{
    [BsonId]
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string LinkCode { get; set; } = default!;

    public bool IsUsed { get; set; }
}
