namespace ProjectFollowUp.BFF.Application.ActivationLinks;

using Microsoft.Extensions.Logging;

internal static partial class GetActivationLinkDataQueryLogs
{
    public static void Started(
        this ILogger<GetActivationLinkDataQueryHandler> logger,
        GetActivationLinkDataQuery query)
    {
        logger.LogOperation(query, (log, identifier) => log.Started(identifier));
    }

    public static void ActivationLinkNotFound(
        this ILogger<GetActivationLinkDataQueryHandler> logger,
        GetActivationLinkDataQuery query)
    {
        logger.LogOperation(query, (log, identifier) => log.ActivationLinkNotFound(identifier));
    }

    public static void ActivationLinkRetrieved(
        this ILogger<GetActivationLinkDataQueryHandler> logger,
        GetActivationLinkDataQuery query)
    {
        logger.LogOperation(query, (log, identifier) => log.ActivationLinkRetrieved(identifier));
    }

    [LoggerMessage(
        EventId = EventIds.UserNotFound,
        EventName = nameof(EventIds.UserNotFound),
        Level = LogLevel.Warning,
        Message = "User not found for activation link id: '{LinkId}'.")]
    public static partial void UserNotFound(
        this ILogger<GetActivationLinkDataQueryHandler> logger,
        Guid linkId);

    [LoggerMessage(
        EventId = EventIds.Completed,
        EventName = nameof(EventIds.Completed),
        Level = LogLevel.Trace,
        Message = "Completed processing GetActivationLinkDataQuery for id: '{LinkId}'.")]
    public static partial void Completed(
        this ILogger<GetActivationLinkDataQueryHandler> logger,
        Guid linkId);

    [LoggerMessage(
        EventId = EventIds.Started,
        EventName = nameof(EventIds.Started),
        Level = LogLevel.Trace,
        Message = "Started processing GetActivationLinkDataQuery for link: '{LinkIdentifier}'.")]
    private static partial void Started(
        this ILogger<GetActivationLinkDataQueryHandler> logger,
        object linkIdentifier);

    [LoggerMessage(
        EventId = EventIds.ActivationLinkNotFound,
        EventName = nameof(EventIds.ActivationLinkNotFound),
        Level = LogLevel.Warning,
        Message = "Activation link not found for link: '{LinkIdentifier}'.")]
    private static partial void ActivationLinkNotFound(
        this ILogger<GetActivationLinkDataQueryHandler> logger,
        object linkIdentifier);

    [LoggerMessage(
        EventId = EventIds.ActivationLinkRetrieved,
        EventName = nameof(EventIds.ActivationLinkRetrieved),
        Level = LogLevel.Debug,
        Message = "Activation link retrieved for link: '{LinkIdentifier}'.")]
    private static partial void ActivationLinkRetrieved(
        this ILogger<GetActivationLinkDataQueryHandler> logger,
        object linkIdentifier);

    private static void LogOperation(
        this ILogger<GetActivationLinkDataQueryHandler> logger,
        GetActivationLinkDataQuery query,
        Action<ILogger<GetActivationLinkDataQueryHandler>, object> logAction)
    {
        switch (query.Mode)
        {
            case GetActivationLinkDataQuery.SearchMode.ByLinkCode:
                logAction(logger, query.LinkCode);
                break;
            case GetActivationLinkDataQuery.SearchMode.ByLinkId:
                logAction(logger, query.LinkId.Value);
                break;
            default:
                logger.LogWarning("Unknown search mode {SearchMode}", query.Mode);
                break;
        }
    }

    private static class EventIds
    {
        public const int Started = 1;

        public const int ActivationLinkNotFound = 2;

        public const int ActivationLinkRetrieved = 3;

        public const int UserNotFound = 4;

        public const int Completed = 5;
    }
}
