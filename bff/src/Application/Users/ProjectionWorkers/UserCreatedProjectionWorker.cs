namespace ProjectFollowUp.BFF.Application.Users.ProjectionWorkers;

using System.Threading;
using System.Threading.Tasks;

using ProjectFollowUp.BFF.Application.EventSourcing;
using ProjectFollowUp.BFF.Application.Users.ReadModel;
using ProjectFollowUp.BFF.Domain.User.Events;

public sealed class UserCreatedProjectionWorker(
    IUserProjectionWriter projectionWriter) : ProjectionWorkerBase<UserCreated>
{
    protected override async Task Materialize(UserCreated domainEvent, CancellationToken cancellationToken)
    {
        var user = await projectionWriter.Get(
            domainEvent.UserId,
            cancellationToken);
        if (user is not null)
        {
            user = user.Value with
            {
                CredentialsId = domainEvent.CredentialsId,
                Email = domainEvent.Email,
                DisplayName = domainEvent.DisplayName,
                CreatedAt = domainEvent.CreatedAt,
            };
        }
        else
        {
            user = new UserRecord(
                domainEvent.UserId,
                domainEvent.CredentialsId,
                domainEvent.Email,
                domainEvent.DisplayName,
                domainEvent.CreatedAt);
        }

        await projectionWriter.Upsert(user.Value, cancellationToken);
    }
}
