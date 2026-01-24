namespace ProjectFollowUp.BFF.Infrastructure.EventBus.Users;

using System.Security.Cryptography;
using System.Threading.Tasks;

using MassTransit;

using ProjectFollowUp.BFF.Application.ActivationLinks;
using ProjectFollowUp.BFF.Application.Cqrs;
using ProjectFollowUp.BFF.Domain.User;

public sealed class CreateActivationLinkConsumer(
    IMediator mediator) : IConsumer<UserRegisteredEvent>
{
    public async Task Consume(ConsumeContext<UserRegisteredEvent> context)
    {
        var linkCode = RandomNumberGenerator.GetString("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789", 16);
        var command = new CreateActivationLinkCommand(
            context.Message.UserId,
            linkCode);
        await mediator.Send(command, context.CancellationToken);
    }
}
