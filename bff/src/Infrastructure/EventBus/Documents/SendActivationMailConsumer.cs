namespace ProjectFollowUp.BFF.Infrastructure.EventBus.Documents;

using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using MimeKit;

using ProjectFollowUp.BFF.Application.ActivationLinks;
using ProjectFollowUp.BFF.Application.Cqrs;
using ProjectFollowUp.BFF.Domain.ActivationLink.DomainEvents;
using ProjectFollowUp.BFF.Infrastructure.MailSender;

using RabbitMQ.Client;

public sealed class SendActivationMailConsumer(
    IMailSender mailSender,
    IChannel channel,
    IMediator mediator) : ConsumerBase<ActivationLinkGenerated>(channel), IConsumer<SendActivationMailConsumer>
{
    public static SendActivationMailConsumer Create(IServiceProvider serviceProvider, IChannel channel)
    {
        var mediator = serviceProvider.GetRequiredService<IMediator>();
        var mailSender = serviceProvider.GetRequiredService<IMailSender>();
        return new SendActivationMailConsumer(
            mailSender,
            channel,
            mediator);
    }

    public async override Task Handle(ActivationLinkGenerated context, CancellationToken cancellationToken)
    {
        var query = new GetActivationLinkDataQuery(context.ActivationLinkId);
        var result = await mediator.Fetch(query, cancellationToken);
        if (result is null)
        {
            return;
        }

        await SendEmail(
            result.LinkCode,
            result.EmailAddress,
            result.DisplayName);
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

        await mailSender.SendEmailAsync(emailMessage, CancellationToken.None);
    }
}
