namespace ProjectFollowUp.BFF.Application.Tasks.ProjectionWorkers;

using ProjectFollowUp.BFF.Application.EventSourcing;
using ProjectFollowUp.BFF.Application.Tasks.ReadModel;

public interface ITaskProjectionWriter : IProjectionWriter<TaskRecord, Guid>
{
}
