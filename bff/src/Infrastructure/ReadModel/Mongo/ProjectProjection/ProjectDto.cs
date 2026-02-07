namespace ProjectFollowUp.BFF.Infrastructure.ReadModel.Mongo.ProjectProjection;

using System.Collections.ObjectModel;

using MongoDB.Bson.Serialization.Attributes;

public sealed class ProjectDto
{
    [BsonId]
    public Guid Id { get; set; }

    public string Title { get; set; } = default!;

    public string Description { get; set; } = default!;

    public DateTimeOffset CreatedAt { get; set; }

    public Collection<AssignedUserDto> AssignedUsers { get; set; } = [];

    public Collection<TaskDto> Tasks { get; set; } = [];

    public sealed class AssignedUserDto
    {
        public Guid Id { get; set; }

        public string DisplayName { get; set; } = default!;
    }

    public sealed class TaskDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = default!;

        public int Status { get; set; }
    }
}
