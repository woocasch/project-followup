namespace ProjectFollowUp.BFF.Infrastructure.EventBus.Documents;

using System.Threading.Tasks;

using MassTransit;

using MimeKit;

using ProjectFollowUp.BFF.Application.EventSourcing;
using ProjectFollowUp.BFF.Domain.ActivationLink;
using ProjectFollowUp.BFF.Domain.User;

public sealed class SendActivationMailConsumer(
    IEventStreamsRepository eventsRepository,
    IAggregateFactory aggregateFactory) : IConsumer<ActivationLinkGeneratedEvent>
{
    public async Task Consume(ConsumeContext<ActivationLinkGeneratedEvent> context)
    {
        var activationLink = await this.GetActivationLink(context);
        var user = await this.GetUser(activationLink.UserId, context.CancellationToken);
        await this.SendEmail(activationLink.LinkCode, user.Email, user.DisplayName);
    }

    private async Task<UserAggregateRoot> GetUser(UserId userId, CancellationToken cancellationToken)
    {
        var userEvents = await eventsRepository.ReadStreamAsync<UserAggregateRoot>(
            userId.ToGuid(),
            cancellationToken);
        var user = aggregateFactory.Create<UserAggregateRoot>(userEvents, UserAggregateRoot.Rehydrate);
        return user;
    }

    private async Task<ActivationLinkAggregateRoot> GetActivationLink(ConsumeContext<ActivationLinkGeneratedEvent> context)
    {
        var activationLinkEvents = await eventsRepository.ReadStreamAsync<ActivationLinkAggregateRoot>(
            context.Message.ActivationLinkId.ToGuid(),
            context.CancellationToken);
        var activationLink = aggregateFactory.Create<ActivationLinkAggregateRoot>(activationLinkEvents, ActivationLinkAggregateRoot.Rehydrate);
        return activationLink;
    }

    private async Task SendEmail(string linkCode, string emailAddress, string displayName)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress("Project Follow-Up Notification", "no-reply@projectfollowup.local"));
        emailMessage.To.Add(new MailboxAddress(displayName, emailAddress));
        emailMessage.Subject = "Activate your Project Follow-Up account";
        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = $"""
            <p>Dear {displayName},</p>
            <p>Please activate your account by clicking the following link:</p>
            <p><a href='http://localhost:4000/account/activate/{linkCode}'>Activate Account</a></p>
            <p>Best regards,<br/>
            Project Follow-Up Team</p>
            """
        };
        emailMessage.Body = bodyBuilder.ToMessageBody();

        using (var client = new MailKit.Net.Smtp.SmtpClient())
        {
            await client.ConnectAsync("localhost", 5007, MailKit.Security.SecureSocketOptions.Auto);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }
}
