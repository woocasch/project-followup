namespace ProjectFollowUp.BFF.Application.Users.ProjectionWorkers;

using ProjectFollowUp.BFF.Application.EventSourcing;
using ProjectFollowUp.BFF.Application.Users.ReadModel;
using ProjectFollowUp.BFF.Domain.User;

public interface IUserProjectionWriter : IProjectionWriter<UserRecord, UserId>
{
}
