namespace ProjectFollowUp.BFF.WebApi.Controllers.Projects;

using Microsoft.Extensions.Logging;

internal static partial class UsersControllerLogs
{
    [LoggerMessage(
        EventId = EventIds.FetchListStarted,
        EventName = nameof(EventIds.FetchListStarted),
        Level = LogLevel.Trace,
        Message = "Fetching users assigned to project '{ProjectId}'.")]
    public static partial void FetchListStarted(
        this ILogger<UsersController> logger,
        Guid projectId);

    [LoggerMessage(
        EventId = EventIds.FetchListDataRetrieved,
        EventName = nameof(EventIds.FetchListDataRetrieved),
        Level = LogLevel.Debug,
        Message = "Data retrieved for fetching users assigned to project '{ProjectId}' with {UserCount} users")]
    public static partial void FetchListDataRetrieved(
        this ILogger<UsersController> logger,
        Guid projectId,
        int userCount);

    [LoggerMessage(
        EventId = EventIds.FetchListResultMapped,
        EventName = nameof(EventIds.FetchListResultMapped),
        Level = LogLevel.Trace,
        Message = "Fetch users result mapped for project '{ProjectId}' with {UserCount} users")]
    public static partial void FetchListResultMapped(
        this ILogger<UsersController> logger,
        Guid projectId,
        int userCount);

     [LoggerMessage(
        EventId = EventIds.FetchListCompleted,
        EventName = nameof(EventIds.FetchListCompleted),
        Level = LogLevel.Trace,
        Message = "Fetch users completed for project '{ProjectId}' with {UserCount} users")]
     public static partial void FetchListCompleted(
         this ILogger<UsersController> logger,
         Guid projectId,
         int userCount);

    private static class EventIds
    {
        public const int FetchListStarted = 1;

        public const int FetchListDataRetrieved = 2;

        public const int FetchListResultMapped = 3;

        public const int FetchListCompleted = 4;
    }
}
