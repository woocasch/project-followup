namespace ProjectFollowUp.BFF.Infrastructure.EventBus.Documents;

using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using MimeKit;

using ProjectFollowUp.BFF.Application.ActivationLinks;
using ProjectFollowUp.BFF.Application.Cqrs;
using ProjectFollowUp.BFF.Domain.ActivationLink.DomainEvents;
using ProjectFollowUp.BFF.Infrastructure.MailSender;

using RabbitMQ.Client;

public sealed class SendActivationMailConsumer(
    IMailSender mailSender,
    IChannel channel,
    IMediator mediator,
    ILogger<SendActivationMailConsumer> logger) : ConsumerBase<ActivationLinkGenerated>(channel, logger), IConsumer<SendActivationMailConsumer>
{
    public static SendActivationMailConsumer Create(IServiceProvider serviceProvider, IChannel channel)
    {
        var mediator = serviceProvider.GetRequiredService<IMediator>();
        var mailSender = serviceProvider.GetRequiredService<IMailSender>();
        var logger = serviceProvider.GetRequiredService<ILogger<SendActivationMailConsumer>>();
        return new SendActivationMailConsumer(
            mailSender,
            channel,
            mediator,
            logger);
    }

    public async override Task Handle(ActivationLinkGenerated context, CancellationToken cancellationToken)
    {
        logger.Started(context.ActivationLinkId.Value);
        var query = GetActivationLinkDataQuery.ByLinkId(context.ActivationLinkId);
        logger.RetrievingLinkInformation(context.ActivationLinkId.Value);
        var result = await mediator.Fetch(query, cancellationToken);
        if (!result.LinkFound)
        {
            logger.LinkNotFound(context.ActivationLinkId.Value);
            return;
        }

        var linkCode = result.Link!;
        logger.SendingEmail(context.ActivationLinkId.Value);
        await SendEmail(
            linkCode.LinkCode,
            linkCode.EmailAddress,
            linkCode.DisplayName);
        logger.Completed(context.ActivationLinkId.Value);
    }

    private async Task SendEmail(string linkCode, string emailAddress, string displayName)
    {
        using var emailMessage = new MimeMessage();
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

        await mailSender.SendEmailAsync(emailMessage, CancellationToken.None);
    }
}
