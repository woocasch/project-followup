namespace ProjectFollowUp.BFF.Infrastructure.ReadModel.Mongo.TaskProjection;

using MongoDB.Bson.Serialization.Attributes;

public sealed class TaskDto
{
    [BsonId]
    public Guid Id { get; set; }

    public Guid ProjectId { get; set; }

    public string Title { get; set; } = default!;

    public string Description { get; set; } = default!;

    public DateOnly? DueDate { get; set; }

    public int Status { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
}
