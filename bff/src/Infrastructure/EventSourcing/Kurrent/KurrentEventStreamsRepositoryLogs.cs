namespace ProjectFollowUp.BFF.Infrastructure.EventSourcing.Kurrent;

using Microsoft.Extensions.Logging;

internal static partial class KurrentEventStreamsRepositoryLogs
{
    [LoggerMessage(
        EventId = EventIds.AppendToStreamStarted,
        EventName = nameof(EventIds.AppendToStreamStarted),
        Level = LogLevel.Trace,
        Message = "Starting append to stream for aggregate {AggregateType} with id {AggregateId}")]
    public static partial void AppendToStreamStarted(
        this ILogger<KurrentEventStreamsRepository> logger,
        Type aggregateType,
        Guid aggregateId);

    [LoggerMessage(
        EventId = EventIds.AppendToStreamEventsCreated,
        EventName = nameof(EventIds.AppendToStreamEventsCreated),
        Level = LogLevel.Debug,
        Message = "Created {EventCount} event(s) for stream {StreamId}")]
    public static partial void AppendToStreamEventsCreated(
        this ILogger<KurrentEventStreamsRepository> logger,
        int eventCount,
        string streamId);

    [LoggerMessage(
        EventId = EventIds.AppendToStreamRevisionCalculated,
        EventName = nameof(EventIds.AppendToStreamRevisionCalculated),
        Level = LogLevel.Trace,
        Message = "Calculated stream revision")]
    public static partial void AppendToStreamRevisionCalculated(
        this ILogger<KurrentEventStreamsRepository> logger);

    [LoggerMessage(
        EventId = EventIds.AppendToStreamStreamIdCalculated,
        EventName = nameof(EventIds.AppendToStreamStreamIdCalculated),
        Level = LogLevel.Trace,
        Message = "Calculated stream id: {StreamId} for aggregate {AggregateType} with id {AggregateId}")]
    public static partial void AppendToStreamStreamIdCalculated(
        this ILogger<KurrentEventStreamsRepository> logger,
        string streamId,
        Type aggregateType,
        Guid aggregateId);

    [LoggerMessage(
        EventId = EventIds.AppendToStreamCompleted,
        EventName = nameof(EventIds.AppendToStreamCompleted),
        Level = LogLevel.Trace,
        Message = "Successfully appended {EventCount} event(s) to stream {StreamId}")]
    public static partial void AppendToStreamCompleted(
        this ILogger<KurrentEventStreamsRepository> logger,
        int eventCount,
        string streamId);

    [LoggerMessage(
        EventId = EventIds.DeleteStreamStarted,
        EventName = nameof(EventIds.DeleteStreamStarted),
        Level = LogLevel.Trace,
        Message = "Starting delete stream for aggregate {AggregateType} with id {AggregateId}")]
    public static partial void DeleteStreamStarted(
        this ILogger<KurrentEventStreamsRepository> logger,
        Type aggregateType,
        Guid aggregateId);

    [LoggerMessage(
        EventId = EventIds.DeleteStreamStreamIdCalculated,
        EventName = nameof(EventIds.DeleteStreamStreamIdCalculated),
        Level = LogLevel.Trace,
        Message = "Calculated stream id: {StreamId} for deletion")]
    public static partial void DeleteStreamStreamIdCalculated(
        this ILogger<KurrentEventStreamsRepository> logger,
        string streamId);

    [LoggerMessage(
        EventId = EventIds.DeleteStreamCompleted,
        EventName = nameof(EventIds.DeleteStreamCompleted),
        Level = LogLevel.Trace,
        Message = "Successfully deleted stream {StreamId}")]
    public static partial void DeleteStreamCompleted(
        this ILogger<KurrentEventStreamsRepository> logger,
        string streamId);

    [LoggerMessage(
        EventId = EventIds.GetStreamVersionStarted,
        EventName = nameof(EventIds.GetStreamVersionStarted),
        Level = LogLevel.Trace,
        Message = "Getting stream version for aggregate {AggregateType} with id {AggregateId}")]
    public static partial void GetStreamVersionStarted(
        this ILogger<KurrentEventStreamsRepository> logger,
        Type aggregateType,
        Guid aggregateId);

    [LoggerMessage(
        EventId = EventIds.GetStreamVersionStreamIdCalculated,
        EventName = nameof(EventIds.GetStreamVersionStreamIdCalculated),
        Level = LogLevel.Trace,
        Message = "Calculated stream id: {StreamId} for version retrieval")]
    public static partial void GetStreamVersionStreamIdCalculated(
        this ILogger<KurrentEventStreamsRepository> logger,
        string streamId);

    [LoggerMessage(
        EventId = EventIds.GetStreamVersionEventsRead,
        EventName = nameof(EventIds.GetStreamVersionEventsRead),
        Level = LogLevel.Trace,
        Message = "Read events from stream {StreamId} for version calculation")]
    public static partial void GetStreamVersionEventsRead(
        this ILogger<KurrentEventStreamsRepository> logger,
        string streamId);

    [LoggerMessage(
        EventId = EventIds.GetStreamVersionStreamNotFound,
        EventName = nameof(EventIds.GetStreamVersionStreamNotFound),
        Level = LogLevel.Warning,
        Message = "Stream {StreamId} not found, returning version 0")]
    public static partial void GetStreamVersionStreamNotFound(
        this ILogger<KurrentEventStreamsRepository> logger,
        string streamId);

    [LoggerMessage(
        EventId = EventIds.GetStreamVersionNoEventsFound,
        EventName = nameof(EventIds.GetStreamVersionNoEventsFound),
        Level = LogLevel.Warning,
        Message = "No events found in stream {StreamId}, returning version 0")]
    public static partial void GetStreamVersionNoEventsFound(
        this ILogger<KurrentEventStreamsRepository> logger,
        string streamId);

    [LoggerMessage(
        EventId = EventIds.GetStreamVersionCompleted,
        EventName = nameof(EventIds.GetStreamVersionCompleted),
        Level = LogLevel.Trace,
        Message = "Stream {StreamId} version is {Version}")]
    public static partial void GetStreamVersionCompleted(
        this ILogger<KurrentEventStreamsRepository> logger,
        string streamId,
        ulong version);

    [LoggerMessage(
        EventId = EventIds.ReadStreamStarted,
        EventName = nameof(EventIds.ReadStreamStarted),
        Level = LogLevel.Trace,
        Message = "Starting read stream for aggregate {AggregateType} with id {AggregateId}")]
    public static partial void ReadStreamStarted(
        this ILogger<KurrentEventStreamsRepository> logger,
        Type aggregateType,
        Guid aggregateId);

    [LoggerMessage(
        EventId = EventIds.ReadStreamStreamIdCalculated,
        EventName = nameof(EventIds.ReadStreamStreamIdCalculated),
        Level = LogLevel.Trace,
        Message = "Calculated stream id: {StreamId} for reading")]
    public static partial void ReadStreamStreamIdCalculated(
        this ILogger<KurrentEventStreamsRepository> logger,
        string streamId);

    [LoggerMessage(
        EventId = EventIds.ReadStreamEventsRead,
        EventName = nameof(EventIds.ReadStreamEventsRead),
        Level = LogLevel.Debug,
        Message = "Read events from stream {StreamId}")]
    public static partial void ReadStreamEventsRead(
        this ILogger<KurrentEventStreamsRepository> logger,
        string streamId);

    [LoggerMessage(
        EventId = EventIds.ReadStreamStreamNotFound,
        EventName = nameof(EventIds.ReadStreamStreamNotFound),
        Level = LogLevel.Warning,
        Message = "Stream {StreamId} not found, returning empty collection")]
    public static partial void ReadStreamStreamNotFound(
        this ILogger<KurrentEventStreamsRepository> logger,
        string streamId);

    [LoggerMessage(
        EventId = EventIds.ReadStreamLoopingOverEvents,
        EventName = nameof(EventIds.ReadStreamLoopingOverEvents),
        Level = LogLevel.Trace,
        Message = "Starting loop over events from stream {StreamId}")]
    public static partial void ReadStreamLoopingOverEvents(
        this ILogger<KurrentEventStreamsRepository> logger,
        string streamId);

    [LoggerMessage(
        EventId = EventIds.ReadStreamDeserializingMetadata,
        EventName = nameof(EventIds.ReadStreamDeserializingMetadata),
        Level = LogLevel.Trace,
        Message = "Deserializing metadata for event {EventNumber}.")]
    public static partial void ReadStreamDeserializingMetadata(
        this ILogger<KurrentEventStreamsRepository> logger,
        ulong eventNumber);

    [LoggerMessage(
        EventId = EventIds.ReadStreamDeserializingEventData,
        EventName = nameof(EventIds.ReadStreamDeserializingEventData),
        Level = LogLevel.Trace,
        Message = "Deserializing event data for event {EventNumber} of type {EventTypeName}")]
    public static partial void ReadStreamDeserializingEventData(
        this ILogger<KurrentEventStreamsRepository> logger,
        ulong eventNumber,
        string eventTypeName);

    [LoggerMessage(
        EventId = EventIds.ReadStreamCreatingEventEnvelope,
        EventName = nameof(EventIds.ReadStreamCreatingEventEnvelope),
        Level = LogLevel.Debug,
        Message = "Created event envelope for {EventTypeName} at stream version {StreamVersion}")]
    public static partial void ReadStreamCreatingEventEnvelope(
        this ILogger<KurrentEventStreamsRepository> logger,
        string eventTypeName,
        ulong streamVersion);

    [LoggerMessage(
        EventId = EventIds.ReadStreamCompleted,
        EventName = nameof(EventIds.ReadStreamCompleted),
        Level = LogLevel.Trace,
        Message = "Completed reading stream {StreamId}, returned {EventCount} event(s)")]
    public static partial void ReadStreamCompleted(
        this ILogger<KurrentEventStreamsRepository> logger,
        string streamId,
        int eventCount);

    [LoggerMessage(
        EventId = EventIds.StreamExistsStarted,
        EventName = nameof(EventIds.StreamExistsStarted),
        Level = LogLevel.Trace,
        Message = "Checking if stream exists for aggregate {AggregateType} with id {AggregateId}")]
    public static partial void StreamExistsStarted(
        this ILogger<KurrentEventStreamsRepository> logger,
        Type aggregateType,
        Guid aggregateId);

    [LoggerMessage(
        EventId = EventIds.StreamExistsStreamIdCalculated,
        EventName = nameof(EventIds.StreamExistsStreamIdCalculated),
        Level = LogLevel.Trace,
        Message = "Calculated stream id: {StreamId} for existence check")]
    public static partial void StreamExistsStreamIdCalculated(
        this ILogger<KurrentEventStreamsRepository> logger,
        string streamId);

    [LoggerMessage(
        EventId = EventIds.StreamExistsStreamRead,
        EventName = nameof(EventIds.StreamExistsStreamRead),
        Level = LogLevel.Trace,
        Message = "Stream {StreamId} read.")]
    public static partial void StreamExistsStreamRead(
        this ILogger<KurrentEventStreamsRepository> logger,
        string streamId);

    [LoggerMessage(
        EventId = EventIds.StreamExistsCompleted,
        EventName = nameof(EventIds.StreamExistsCompleted),
        Level = LogLevel.Trace,
        Message = "Stream {StreamId} exists: {Exists}")]
    public static partial void StreamExistsCompleted(
        this ILogger<KurrentEventStreamsRepository> logger,
        string streamId,
        bool exists);

    private static class EventIds
    {
        public const int AppendToStreamStarted = 1;

        public const int AppendToStreamEventsCreated = 2;

        public const int AppendToStreamRevisionCalculated = 3;

        public const int AppendToStreamStreamIdCalculated = 4;

        public const int AppendToStreamCompleted = 5;

        public const int DeleteStreamStarted = 6;

        public const int DeleteStreamStreamIdCalculated = 7;

        public const int DeleteStreamCompleted = 8;

        public const int GetStreamVersionStarted = 9;

        public const int GetStreamVersionStreamIdCalculated = 10;

        public const int GetStreamVersionEventsRead = 11;

        public const int GetStreamVersionStreamNotFound = 12;

        public const int GetStreamVersionNoEventsFound = 13;

        public const int GetStreamVersionCompleted = 14;

        public const int ReadStreamStarted = 15;

        public const int ReadStreamStreamIdCalculated = 16;

        public const int ReadStreamEventsRead = 17;

        public const int ReadStreamStreamNotFound = 18;

        public const int ReadStreamLoopingOverEvents = 19;

        public const int ReadStreamDeserializingMetadata = 20;

        public const int ReadStreamDeserializingEventData = 21;

        public const int ReadStreamCreatingEventEnvelope = 22;

        public const int ReadStreamCompleted = 23;

        public const int StreamExistsStarted = 24;

        public const int StreamExistsStreamIdCalculated = 25;

        public const int StreamExistsStreamRead = 26;

        public const int StreamExistsCompleted = 27;
    }
}
