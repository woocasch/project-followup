namespace ProjectFollowUp.BFF.Application.Users.ProjectionWorkers;

using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using ProjectFollowUp.BFF.Application.EventSourcing;
using ProjectFollowUp.BFF.Application.Users.ReadModel;
using ProjectFollowUp.BFF.Domain.User.Events;

public sealed class UserCreatedProjectionWorker(
    IUserProjectionWriter projectionWriter,
    ILogger<UserCreatedProjectionWorker> logger)
    : ProjectionWorkerBase<UserCreated>
{
    protected override async Task Materialize(UserCreated domainEvent, CancellationToken cancellationToken)
    {
        logger.Started(domainEvent.UserId.Value);
        var user = await projectionWriter.Get(
            domainEvent.UserId,
            cancellationToken);
        if (user is not null)
        {
            logger.UserExists(domainEvent.UserId.Value);
            user = user.Value with
            {
                CredentialsId = domainEvent.CredentialsId,
                Email = domainEvent.Email,
                DisplayName = domainEvent.DisplayName,
                CreatedAt = domainEvent.CreatedAt,
            };
            logger.UserUpdated(domainEvent.UserId.Value);
            await projectionWriter.Update(user.Value, cancellationToken);
        }
        else
        {
            logger.UserNotExists(domainEvent.UserId.Value);
            user = new UserRecord(
                domainEvent.UserId,
                domainEvent.CredentialsId,
                domainEvent.Email,
                domainEvent.DisplayName,
                domainEvent.CreatedAt);
            logger.UserCreated(domainEvent.UserId.Value);
            await projectionWriter.Insert(user.Value, cancellationToken);
        }

        logger.Completed(domainEvent.UserId.Value);
    }
}
