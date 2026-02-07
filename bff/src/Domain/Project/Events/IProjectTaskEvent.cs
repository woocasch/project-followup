namespace ProjectFollowUp.BFF.Domain.Project.Events;

public interface IProjectTaskEvent
{
    ProjectId ProjectId { get; }

    Guid TaskId { get; }
}
