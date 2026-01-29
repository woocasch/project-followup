namespace ProjectFollowUp.BFF.Infrastructure.MailSender;

using MimeKit;

public interface IMailSender
{
    Task SendEmailAsync(
        MimeMessage message,
        CancellationToken cancellationToken);
}
