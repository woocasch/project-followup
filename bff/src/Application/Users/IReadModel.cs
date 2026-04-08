namespace ProjectFollowUp.BFF.Application.Users;

using ProjectFollowUp.BFF.Application.Users.ReadModel;
using ProjectFollowUp.BFF.Domain.User;

public interface IReadModel
{
    Task<UserRecord?> Get(UserId userId, CancellationToken cancellationToken);

    Task<UserRecord?> GetByEmail(string userName, CancellationToken cancellationToken);
}
