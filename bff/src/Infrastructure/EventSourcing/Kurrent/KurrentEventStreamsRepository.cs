namespace ProjectFollowUp.BFF.Infrastructure.EventSourcing.Kurrent;

using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using Google.Protobuf.Compiler;

using KurrentDB.Client;

using Microsoft.Extensions.Logging;

using ProjectFollowUp.BFF.Application.EventSourcing;
using ProjectFollowUp.BFF.Domain;
using ProjectFollowUp.BFF.Infrastructure.Serialization.Json;

public sealed class KurrentEventStreamsRepository(
    KurrentDBClient eventStoreClient,
    INamingService namingService,
    ILogger<KurrentEventStreamsRepository> logger) : IEventStreamsRepository
{
    private static readonly JsonSerializerOptions serializerOptions = JsonSerializerOptionsFactory.GetOptions();

    public async Task AppendToStreamAsync<TAggregate>(
        Guid aggregateId,
        IEnumerable<IAggregateEvent> events,
        ulong expectedVersion,
        CancellationToken cancellationToken)
        where TAggregate : class
    {
        logger.AppendToStreamStarted(typeof(TAggregate), aggregateId);
        var streamId = namingService.GetStreamName<TAggregate>(aggregateId);
        logger.AppendToStreamStreamIdCalculated(streamId, typeof(TAggregate), aggregateId);
        var eventData = events.Select(e => new EventData(
            eventId: Uuid.NewUuid(),
            type: e.GetType().Name,
            data: Encoding.UTF8.GetBytes(JsonSerializer.Serialize(e, e.GetType(), serializerOptions)),
            metadata: Encoding.UTF8.GetBytes(JsonSerializer.Serialize(new EventMetadata(e.GetType().AssemblyQualifiedName!), serializerOptions))
        ))
            .ToList();
        logger.AppendToStreamEventsCreated(eventData.Count, streamId);

        var streamRevision = expectedVersion == 0
            ? StreamState.NoStream
            : expectedVersion - 1;
        logger.AppendToStreamRevisionCalculated();

        await eventStoreClient.AppendToStreamAsync(
            streamId,
            streamRevision,
            eventData,
            cancellationToken: cancellationToken);
        logger.AppendToStreamCompleted(eventData.Count, streamId);
    }

    public async Task DeleteStreamAsync<TAggregate>(Guid aggregateId, CancellationToken cancellationToken)
        where TAggregate : class
    {
        logger.DeleteStreamStarted(typeof(TAggregate), aggregateId);
        var streamId = namingService.GetStreamName<TAggregate>(aggregateId);
        logger.DeleteStreamStreamIdCalculated(streamId);
        await eventStoreClient.DeleteAsync(
            streamId,
            StreamState.Any,
            cancellationToken: cancellationToken);
        logger.DeleteStreamCompleted(streamId);
    }

    public async Task<ulong> GetStreamVersionAsync<TAggregate>(Guid aggregateId, CancellationToken cancellationToken)
        where TAggregate : class
    {
        logger.GetStreamVersionStarted(typeof(TAggregate), aggregateId);
        var streamId = namingService.GetStreamName<TAggregate>(aggregateId);
        logger.GetStreamVersionStreamIdCalculated(streamId);
        var result = eventStoreClient.ReadStreamAsync(
            Direction.Backwards,
            streamId,
            StreamPosition.End,
            maxCount: 1,
            cancellationToken: cancellationToken);
        logger.GetStreamVersionEventsRead(streamId);

        var state = await result.ReadState;

        if (state == ReadState.StreamNotFound)
        {
            logger.GetStreamVersionStreamNotFound(streamId);
            return 0;
        }

        var events = await result.ToListAsync(cancellationToken);
        if (events.Count == 0)
        {
            logger.GetStreamVersionNoEventsFound(streamId);
            return 0;
        }

        var version = (ulong)events[0].Event.EventNumber.ToInt64() + 1;
        logger.GetStreamVersionCompleted(streamId, version);
        return version;
    }

    public async Task<IEnumerable<EventEnvelope>> ReadStreamAsync<TAggregate>(Guid aggregateId, CancellationToken cancellationToken)
        where TAggregate : class
    {
        logger.ReadStreamStarted(typeof(TAggregate), aggregateId);
        var streamId = namingService.GetStreamName<TAggregate>(aggregateId);
        logger.ReadStreamStreamIdCalculated(streamId);
        var result = eventStoreClient.ReadStreamAsync(
            Direction.Forwards,
            streamId,
            StreamPosition.Start,
            cancellationToken: cancellationToken);
        logger.ReadStreamEventsRead(streamId);

        var state = await result.ReadState;

        if (state == ReadState.StreamNotFound)
        {
            logger.ReadStreamStreamNotFound(streamId);
            return [];
        }

        var events = new List<EventEnvelope>();
        var streamType = ExtractStreamType(streamId);

        logger.ReadStreamLoopingOverEvents(streamId);
        await foreach (var resolvedEvent in result)
        {
            logger.ReadStreamDeserializingMetadata(resolvedEvent.OriginalEvent.EventNumber);
            var eventMetadata = JsonSerializer.Deserialize<EventMetadata?>(
                Encoding.UTF8.GetString(resolvedEvent.Event.Metadata.Span),
                serializerOptions);

            if (string.IsNullOrWhiteSpace(eventMetadata?.EventTypeName))
            {
                continue;
            }

            logger.ReadStreamDeserializingEventData(resolvedEvent.OriginalEventNumber, eventMetadata.Value.EventTypeName);
            var eventTypeName = eventMetadata.Value.EventTypeName;
            var eventType = Type.GetType(eventTypeName);
            if (eventType == null)
            {
                continue;
            }

            if (!eventType.IsAssignableTo(typeof(IAggregateEvent)))
            {
                continue;
            }

            logger.ReadStreamCreatingEventEnvelope(eventMetadata.Value.EventTypeName, resolvedEvent.OriginalEvent.EventNumber);
            var eventData = (IAggregateEvent?)JsonSerializer.Deserialize(
                Encoding.UTF8.GetString(resolvedEvent.Event.Data.Span),
                eventType,
                serializerOptions);

            if (eventData == null)
            {
                continue;
            }

            var envelope = new EventEnvelope(
                Event: eventData,
                StreamType: streamType,
                StreamId: streamId,
                StreamVersion: (ulong)resolvedEvent.Event.EventNumber.ToInt64() + 1,
                Timestamp: resolvedEvent.Event.Created,
                EventTypeName: resolvedEvent.Event.EventType);

            events.Add(envelope);
        }

        logger.ReadStreamCompleted(streamId, events.Count);
        return events;
    }

    public async Task<bool> StreamExistsAsync<TAggregate>(Guid aggregateId, CancellationToken cancellationToken)
        where TAggregate : class
    {
        logger.StreamExistsStarted(typeof(TAggregate), aggregateId);
        var streamId = namingService.GetStreamName<TAggregate>(aggregateId);
        logger.StreamExistsStreamIdCalculated(streamId);
        var result = eventStoreClient.ReadStreamAsync(
            Direction.Forwards,
            streamId,
            StreamPosition.Start,
            maxCount: 1,
            cancellationToken: cancellationToken);
        logger.StreamExistsStreamRead(streamId);

        var state = await result.ReadState;

        var streamExists = state != ReadState.StreamNotFound;
        logger.StreamExistsCompleted(streamId, streamExists);
        return streamExists;
    }

    private static string ExtractStreamType(string aggregateId)
    {
        var parts = aggregateId.Split('-', 2);
        return parts.Length > 0 ? parts[0] : "Unknown";
    }
}
