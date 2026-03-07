namespace ProjectFollowUp.BFF.Infrastructure.MailSender;

using Microsoft.Extensions.Logging;

internal static partial class SmtpMailSenderLogs
{
    [LoggerMessage(
        EventId = EventIds.SendEmailStarted,
        EventName = nameof(EventIds.SendEmailStarted),
        Level = LogLevel.Trace,
        Message = "Starting to send email.")]
    public static partial void SendEmailStarted(
        this ILogger<SmtpMailSender> logger);

    [LoggerMessage(
        EventId = EventIds.SendEmailConnected,
        EventName = nameof(EventIds.SendEmailConnected),
        Level = LogLevel.Debug,
        Message = "Connected to SMTP server {Host}:{Port}.")]
    public static partial void SendEmailConnected(
        this ILogger<SmtpMailSender> logger,
        string host,
        int port);

    [LoggerMessage(
        EventId = EventIds.SendEmailAuthenticated,
        EventName = nameof(EventIds.SendEmailAuthenticated),
        Level = LogLevel.Debug,
        Message = "Authenticated to SMTP server as {Username}.")]
    public static partial void SendEmailAuthenticated(
        this ILogger<SmtpMailSender> logger,
        string username);

    [LoggerMessage(
        EventId = EventIds.SendEmailSent,
        EventName = nameof(EventIds.SendEmailSent),
        Level = LogLevel.Trace,
        Message = "Email sent to {Recipient}.")]
    public static partial void SendEmailSent(
        this ILogger<SmtpMailSender> logger,
        string recipient);

    [LoggerMessage(
        EventId = EventIds.SendEmailDisconnected,
        EventName = nameof(EventIds.SendEmailDisconnected),
        Level = LogLevel.Debug,
        Message = "Disconnected from SMTP server.")]
    public static partial void SendEmailDisconnected(
        this ILogger<SmtpMailSender> logger);

    private static class EventIds
    {
        public const int SendEmailStarted = 1;

        public const int SendEmailConnected = 2;

        public const int SendEmailAuthenticated = 3;

        public const int SendEmailSent = 4;

        public const int SendEmailDisconnected = 5;
    }
}
