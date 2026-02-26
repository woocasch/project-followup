namespace ProjectFollowUp.BFF.Infrastructure.MailSender;

using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using MimeKit;

public sealed class SmtpMailSender(
    IOptions<MailSettings> mailSettingsOptions,
    ILogger<SmtpMailSender> logger) : IMailSender
{
    private readonly MailSettings mailSettings = mailSettingsOptions.Value;

    public async Task SendEmailAsync(MimeMessage message, CancellationToken cancellationToken)
    {
#pragma warning disable CA1873 // Avoid potentially expensive logging
        logger.SendEmailStarted(message.To.ToString());
#pragma warning restore CA1873 // Avoid potentially expensive logging
        using var client = new MailKit.Net.Smtp.SmtpClient();
        client.ServerCertificateValidationCallback = (s, c, h, e) => true;
        await client.ConnectAsync(
            this.mailSettings.Host,
            this.mailSettings.Port,
            MailKit.Security.SecureSocketOptions.Auto,
            cancellationToken);
        logger.SendEmailConnected(this.mailSettings.Host, this.mailSettings.Port);
        await client.AuthenticateAsync(
            mailSettings.Username,
            mailSettings.Password,
            cancellationToken);
        logger.SendEmailAuthenticated(this.mailSettings.Username);
        await client.SendAsync(
            message);
#pragma warning disable CA1873 // Avoid potentially expensive logging
        logger.SendEmailSent(message.To.ToString());
#pragma warning restore CA1873 // Avoid potentially expensive logging
        await client.DisconnectAsync(
            true,
            cancellationToken);
        logger.SendEmailDisconnected();
    }
}
