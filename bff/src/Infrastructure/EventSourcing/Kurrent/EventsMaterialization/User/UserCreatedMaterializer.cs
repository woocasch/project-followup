namespace ProjectFollowUp.BFF.Infrastructure.EventSourcing.Kurrent.EventsMaterialization.User;

using System.Threading;
using System.Threading.Tasks;

using ProjectFollowUp.BFF.Domain.User.Events;
using ProjectFollowUp.BFF.Infrastructure.EventSourcing.ReadModel;

public sealed class UserCreatedMaterializer(
    IUserManager userManager) : EventMaterializerBase<UserCreated>
{
    protected override async Task MaterializeEvent(UserCreated @event, CancellationToken cancellationToken)
    {
        var user = await userManager.GetCurrent(
            @event.UserId.ToGuid(),
            cancellationToken);
        if (user is not null)
        {
            user = user with
            {
                CredentialsId = @event.CredentialsId,
                Email = @event.Email.Value,
                DisplayName = @event.DisplayName,
                CreatedAt = @event.CreatedAt,
            };
        }
        else
        {
            user = new ReadModel.User.UserRecord(
            @event.UserId.ToGuid(),
            @event.CredentialsId,
            @event.Email.Value,
            @event.DisplayName,
            @event.CreatedAt);
        }

        await userManager.Upsert(user, cancellationToken);
    }
}
