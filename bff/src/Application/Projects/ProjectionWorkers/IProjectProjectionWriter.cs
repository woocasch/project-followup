namespace ProjectFollowUp.BFF.Application.Projects.ProjectionWorkers;

using ProjectFollowUp.BFF.Application.EventSourcing;
using ProjectFollowUp.BFF.Application.Projects.ReadModel;
using ProjectFollowUp.BFF.Domain.Project;

public interface IProjectProjectionWriter : IProjectionWriter<ProjectRecord, ProjectId>
{
}
