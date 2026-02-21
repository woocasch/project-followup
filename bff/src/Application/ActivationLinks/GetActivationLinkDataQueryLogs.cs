namespace ProjectFollowUp.BFF.Application.ActivationLinks;

using Microsoft.Extensions.Logging;

using ProjectFollowUp.BFF.Dictionaries.Logging;

public static partial class GetActivationLinkDataQueryLogs
{
    [LoggerMessage(
        EventId = ActivationLinks.GetActivationLinkDataQuery_Started,
        Level = LogLevel.Trace,
        Message = "Started processing GetActivationLinkDataQuery for link: '{LinkIdentifier}'.")]
    public static partial void Started(
        this ILogger<GetActivationLinkDataQueryHandler> logger,
        object linkIdentifier);

    [LoggerMessage(
        EventId = ActivationLinks.GetActivationLinkDataQuery_ActivationLinkNotFound,
        Level = LogLevel.Warning,
        Message = "Activation link not found for link: '{LinkIdentifier}'.")]
    public static partial void ActivationLinkNotFound(
        this ILogger<GetActivationLinkDataQueryHandler> logger,
        object linkIdentifier);

    [LoggerMessage(
        EventId = ActivationLinks.GetActivationLinkDataQuery_ActivationLinkRetrieved,
        Level = LogLevel.Debug,
        Message = "Activation link retrieved for link: '{LinkIdentifier}'.")] 
    public static partial void ActivationLinkRetrieved(
        this ILogger<GetActivationLinkDataQueryHandler> logger,
        object linkIdentifier);

    [LoggerMessage(
        EventId = ActivationLinks.GetActivationLinkDataQuery_UserNotFound,
        Level = LogLevel.Warning,
        Message = "User not found for activation link id: '{LinkId}'.")]
    public static partial void UserNotFound(
        this ILogger<GetActivationLinkDataQueryHandler> logger,
        Guid linkId);

    [LoggerMessage(
        EventId = ActivationLinks.GetActivationLinkDataQuery_Completed,
        Level = LogLevel.Trace,
        Message = "Completed processing GetActivationLinkDataQuery for id: '{LinkId}'.")]
    public static partial void Completed(
        this ILogger logger,
        Guid linkId);

    internal static object GetLinkIdentifier(
        this GetActivationLinkDataQuery query)
    {
        return query.Mode switch
        {
            GetActivationLinkDataQuery.SearchMode.ByLinkCode => query.LinkCode,
            GetActivationLinkDataQuery.SearchMode.ByLinkId => query.LinkId.Value,
            _ => "unknown"
        };
    }
}
