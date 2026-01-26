namespace ProjectFollowUp.BFF.Infrastructure.MailSender;

using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Options;

using MimeKit;

public sealed class SmtpMailSender(
    IOptions<MailSettings> mailSettingsOptions) : IMailSender
{
    private readonly MailSettings mailSettings = mailSettingsOptions.Value;

    public async Task SendEmailAsync(MimeMessage message, CancellationToken cancellationToken)
    {
        using var client = new MailKit.Net.Smtp.SmtpClient();
        client.ServerCertificateValidationCallback = (s, c, h, e) => true;
        await client.ConnectAsync(
            this.mailSettings.Host,
            this.mailSettings.Port,
            MailKit.Security.SecureSocketOptions.Auto,
            cancellationToken);
        await client.AuthenticateAsync(
            mailSettings.Username,
            mailSettings.Password,
            cancellationToken);
        await client.SendAsync(
            message);
        await client.DisconnectAsync(
            true,
            cancellationToken);
    }
}
