namespace ProjectFollowUp.BFF.Infrastructure.EventBus.ActivationLinks;

using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using ProjectFollowUp.BFF.Application.ActivationLinks;
using ProjectFollowUp.BFF.Application.Cqrs;
using ProjectFollowUp.BFF.Domain.User.DomainEvents;

using RabbitMQ.Client;

public sealed class CreateActivationLinkConsumer(
    IChannel channel,
    IMediator mediator,
    ILogger<CreateActivationLinkConsumer> logger) : ConsumerBase<UserRegistered>(channel, logger), IConsumer<CreateActivationLinkConsumer>
{
    public static CreateActivationLinkConsumer Create(IServiceProvider serviceProvider, IChannel channel)
    {
        var mediator = serviceProvider.GetRequiredService<IMediator>();
        var logger = serviceProvider.GetRequiredService<ILogger<CreateActivationLinkConsumer>>();
        return new CreateActivationLinkConsumer(
            channel,
            mediator,
            logger);
    }

    public async override Task Handle(UserRegistered context, CancellationToken cancellationToken)
    {
        var linkCode = RandomNumberGenerator.GetString("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789", 16);
        var command = new CreateActivationLinkCommand(
            context.UserId,
            linkCode);
        await mediator.Send(command, CancellationToken.None);
    }
}
