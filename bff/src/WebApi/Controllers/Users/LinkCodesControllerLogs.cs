namespace ProjectFollowUp.BFF.WebApi.Controllers.Users;

internal static partial class LinkCodesControllerLogs
{
    [LoggerMessage(
    EventId = EventIds.GetByLinkCodeStarted,
    EventName = nameof(EventIds.GetByLinkCodeStarted),
    Level = LogLevel.Trace,
    Message = "Started retrieving link code: {LinkCode}")]
    public static partial void GetByLinkCodeStarted(
    this ILogger<LinkCodesController> logger,
    string linkCode);

    [LoggerMessage(
        EventId = EventIds.GetByLinkCodeDataRetrieved,
        EventName = nameof(EventIds.GetByLinkCodeDataRetrieved),
        Level = LogLevel.Trace,
        Message = "Data retrieved for link code: {LinkCode}")]
    public static partial void GetByLinkCodeDataRetrieved(
        this ILogger<LinkCodesController> logger,
        string linkCode);

    [LoggerMessage(
        EventId = EventIds.GetByLinkCodeLinkCodeNotFound,
        EventName = nameof(EventIds.GetByLinkCodeLinkCodeNotFound),
        Level = LogLevel.Warning,
        Message = "Link code not found: {LinkCode}")]
    public static partial void GetByLinkCodeLinkCodeNotFound(
        this ILogger<LinkCodesController> logger,
        string linkCode);

    [LoggerMessage(
        EventId = EventIds.GetByLinkCodeCompleted,
        EventName = nameof(EventIds.GetByLinkCodeCompleted),
        Level = LogLevel.Trace,
        Message = "Completed retrieving link code: {LinkCode}")]
    public static partial void GetByLinkCodeCompleted(
        this ILogger<LinkCodesController> logger,
        string linkCode);

    private static class EventIds
    {
        public const int GetByLinkCodeStarted = 1;

        public const int GetByLinkCodeDataRetrieved = 2;

        public const int GetByLinkCodeLinkCodeNotFound = 3;

        public const int GetByLinkCodeCompleted = 4;
    }
}
