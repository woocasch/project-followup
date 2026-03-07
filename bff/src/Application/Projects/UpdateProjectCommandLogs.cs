namespace ProjectFollowUp.BFF.Application.Projects;

using Microsoft.Extensions.Logging;

internal static partial class UpdateProjectCommandLogs
{
    [LoggerMessage(
        EventId = EventIds.Started,
        EventName = nameof(EventIds.Started),
        Level = LogLevel.Trace,
        Message = "Started project update command for project with id {ProjectId}.")]
    public static partial void Started(
        this ILogger<UpdateProjectCommandHandler> logger,
        Guid projectId);

    [LoggerMessage(
        EventId = EventIds.AggregateRehydrated,
        EventName = nameof(EventIds.AggregateRehydrated),
        Level = LogLevel.Trace,
        Message = "Project {ProjectId} aggregate rehydrated.")]
    public static partial void AggregateRehydrated(
        this ILogger<UpdateProjectCommandHandler> logger,
        Guid projectId);

    [LoggerMessage(
        EventId = EventIds.ProjectUpdated,
        EventName = nameof(EventIds.ProjectUpdated),
        Level = LogLevel.Debug,
        Message = "Project {ProjectId} updated.")]
    public static partial void ProjectUpdated(
        this ILogger<UpdateProjectCommandHandler> logger,
        Guid projectId);

    [LoggerMessage(
        EventId = EventIds.Completed,
        EventName = nameof(EventIds.Completed),
        Level = LogLevel.Trace,
        Message = "Completed project update command for project with id {ProjectId}.")]
    public static partial void Completed(
        this ILogger<UpdateProjectCommandHandler> logger,
        Guid projectId);

    private static class EventIds
    {
        public const int Started = 1;

        public const int AggregateRehydrated = 2;

        public const int ProjectUpdated = 3;

        public const int Completed = 4;
    }
}
