namespace ProjectFollowUp.BFF.Infrastructure.ReadModel.Mongo.ProjectProjection;

using MongoDB.Bson.Serialization.Attributes;

public sealed class ProjectDto
{
    [BsonId]
    public Guid Id { get; set; }

    public string Title { get; set; } = default!;

    public string Description { get; set; } = default!;

    public DateTimeOffset CreatedAt { get; set; }
}
