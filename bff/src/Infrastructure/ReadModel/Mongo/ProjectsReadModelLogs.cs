namespace ProjectFollowUp.BFF.Infrastructure.ReadModel.Mongo;

using Microsoft.Extensions.Logging;

internal static partial class ProjectsReadModelLogs
{
    [LoggerMessage(
        EventId = EventIds.FetchStarted,
        EventName = nameof(EventIds.FetchStarted),
        Level = LogLevel.Trace,
        Message = "Fetch projects from read model started.")]
    public static partial void FetchStarted(
        this ILogger<ProjectsReadModel> logger);

    [LoggerMessage(
        EventId = EventIds.FetchCallingStorage,
        EventName = nameof(EventIds.FetchCallingStorage),
        Level = LogLevel.Debug,
        Message = "Fetch projects from read model: calling storage.")]
    public static partial void FetchCallingStorage(
        this ILogger<ProjectsReadModel> logger);

    [LoggerMessage(
        EventId = EventIds.FetchMappingResults,
        EventName = nameof(EventIds.FetchMappingResults),
        Level = LogLevel.Trace,
        Message = "Fetch projects from read model: mapping results.")]
    public static partial void FetchMappingResults(
        this ILogger<ProjectsReadModel> logger);

    [LoggerMessage(
        EventId = EventIds.FetchCompleted,
        EventName = nameof(EventIds.FetchCompleted),
        Level = LogLevel.Trace,
        Message = "Fetch projects from read model completed.")]
    public static partial void FetchCompleted(
        this ILogger<ProjectsReadModel> logger);

    [LoggerMessage(
        EventId = EventIds.GetStarted,
        EventName = nameof(EventIds.GetStarted),
        Level = LogLevel.Trace,
        Message = "Get project from read model started.")]
    public static partial void GetStarted(
        this ILogger<ProjectsReadModel> logger);

    [LoggerMessage(
        EventId = EventIds.GetCallingStorage,
        EventName = nameof(EventIds.GetCallingStorage),
        Level = LogLevel.Debug,
        Message = "Get project from read model: calling storage.")]
    public static partial void GetCallingStorage(
        this ILogger<ProjectsReadModel> logger);

    [LoggerMessage(
        EventId = EventIds.GetNoItemsFound,
        EventName = nameof(EventIds.GetNoItemsFound),
        Level = LogLevel.Warning,
        Message = "Get project from read model: no items found.")]
    public static partial void GetNoItemsFound(
        this ILogger<ProjectsReadModel> logger);

    [LoggerMessage(
        EventId = EventIds.GetCompleted,
        EventName = nameof(EventIds.GetCompleted),
        Level = LogLevel.Trace,
        Message = "Get project from read model completed.")]
    public static partial void GetCompleted(
        this ILogger<ProjectsReadModel> logger);

    private static class EventIds
    {
        public const int FetchStarted = 1;

        public const int FetchCallingStorage = 2;

        public const int FetchMappingResults = 3;

        public const int FetchCompleted = 4;

        public const int GetStarted = 5;

        public const int GetCallingStorage = 6;

        public const int GetNoItemsFound = 7;

        public const int GetCompleted = 8;
    }
}
