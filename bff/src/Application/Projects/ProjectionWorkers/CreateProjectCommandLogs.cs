namespace ProjectFollowUp.BFF.Application.Projects.ProjectionWorkers;

using Microsoft.Extensions.Logging;

internal static partial class CreateProjectCommandLogs
{
    [LoggerMessage(
        EventId = EventIds.Started,
        EventName = nameof(EventIds.Started),
        Level = LogLevel.Trace,
        Message = "Started processing CreateProjectCommand for ProjectId: {ProjectId}.")]
    public static partial void Started(
        this ILogger<CreateProjectCommandHandler> logger,
        Guid projectId);

    [LoggerMessage(
        EventId = EventIds.StoringNewStream,
        EventName = nameof(EventIds.StoringNewStream),
        Level = LogLevel.Debug,
        Message = "Storing new event stream for ProjectId: {ProjectId}.")]
    public static partial void StoringNewStream(
        this ILogger<CreateProjectCommandHandler> logger,
        Guid projectId);

    [LoggerMessage(
        EventId = EventIds.Completed,
        EventName = nameof(EventIds.Completed),
        Level = LogLevel.Trace,
        Message = "Completed processing CreateProjectCommand for ProjectId: {ProjectId}.")]
    public static partial void Completed(
        this ILogger<CreateProjectCommandHandler> logger,
        Guid projectId);

    private static class EventIds
    {
        public const int Started = 1;

        public const int StoringNewStream = 2;

        public const int Completed = 3;
    }
}
