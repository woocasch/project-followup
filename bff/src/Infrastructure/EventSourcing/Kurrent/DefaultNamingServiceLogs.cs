namespace ProjectFollowUp.BFF.Infrastructure.EventSourcing.Kurrent;

using Microsoft.Extensions.Logging;

internal static partial class DefaultNamingServiceLogs
{
    [LoggerMessage(
        EventId = EventIds.GetStreamNameStarted,
        EventName = nameof(EventIds.GetStreamNameStarted),
        Level = LogLevel.Trace,
        Message = "Getting stream name for aggregate type {AggregateType} and aggregate id {AggregateId}.")]
    public static partial void GetStreamNameStarted(
        this ILogger<DefaultNamingService> logger,
        Type aggregateType,
        string aggregateId);

    [LoggerMessage(
        EventId = EventIds.StreamNameFound,
        EventName = nameof(EventIds.StreamNameFound),
        Level = LogLevel.Trace,
        Message = "Stream name {StreamName} found for aggregate type {AggregateType} and aggregate id {AggregateId}.")]
    public static partial void StreamNameFound(
        this ILogger<DefaultNamingService> logger,
        string streamName,
        Type aggregateType,
        string aggregateId);

    [LoggerMessage(
        EventId = EventIds.GetMappingKeyStarted,
        EventName = nameof(EventIds.GetMappingKeyStarted),
        Level = LogLevel.Trace,
        Message = "Getting mapping key for aggregate type {AggregateType}.")]
    public static partial void GetMappingKeyStarted(
        this ILogger<DefaultNamingService> logger,
        Type aggregateType);

    [LoggerMessage(
        EventId = EventIds.MappingKeyFound,
        EventName = nameof(EventIds.MappingKeyFound),
        Level = LogLevel.Trace,
        Message = "Mapping key {MappingKey} found for aggregate type {AggregateType}.")]
    public static partial void MappingKeyFound(
        this ILogger<DefaultNamingService> logger,
        string mappingKey,
        Type aggregateType);

    private static class EventIds
    {
        public const int GetStreamNameStarted = 1;

        public const int StreamNameFound = 2;

        public const int GetMappingKeyStarted = 3;

        public const int MappingKeyFound = 4;
    }
}
