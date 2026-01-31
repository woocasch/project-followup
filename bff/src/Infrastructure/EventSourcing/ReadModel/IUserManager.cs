namespace ProjectFollowUp.BFF.Infrastructure.EventSourcing.ReadModel;

using ProjectFollowUp.BFF.Infrastructure.EventSourcing.ReadModel.User;

public interface IUserManager
{
    Task<UserRecord?> GetCurrent(
        Guid id,
        CancellationToken cancellationToken);

    Task Upsert(
        UserRecord user,
        CancellationToken cancellationToken);
}
