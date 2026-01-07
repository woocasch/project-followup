namespace ProjectFollowUp.BFF.Infrastructure.EventSourcing.Kurrent;

using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using KurrentDB.Client;

using ProjectFollowUp.BFF.Application.EventSourcing;

public sealed class KurrentEventStreamsRepository(
    KurrentDBClient eventStoreClient,
    INamingService namingService) : IEventStreamsRepository
{
    private static readonly JsonSerializerOptions serializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false,
    };

    public async Task AppendToStreamAsync<TAggregate>(
        string aggregateId,
        IEnumerable<object> events,
        ulong expectedVersion,
        CancellationToken cancellationToken)
        where TAggregate : class
    {
        var eventData = events.Select(e => new EventData(
            eventId: Uuid.NewUuid(),
            type: e.GetType().Name,
            data: Encoding.UTF8.GetBytes(JsonSerializer.Serialize(e, e.GetType(), serializerOptions)),
            metadata: Encoding.UTF8.GetBytes(JsonSerializer.Serialize(new EventMetadata(e.GetType().AssemblyQualifiedName!), serializerOptions))
        ));

        var streamRevision = expectedVersion == 0
            ? StreamState.NoStream
            : expectedVersion - 1;

        var streamId = namingService.GetStreamName<TAggregate>(aggregateId);
        await eventStoreClient.AppendToStreamAsync(
            streamId,
            streamRevision,
            eventData,
            cancellationToken: cancellationToken);
    }

    public async Task DeleteStreamAsync(string aggregateId, CancellationToken cancellationToken)
    {
        await eventStoreClient.DeleteAsync(
            aggregateId,
            StreamState.Any,
            cancellationToken: cancellationToken);
    }

    public async Task<ulong> GetStreamVersionAsync(string aggregateId, CancellationToken cancellationToken)
    {
        var result = eventStoreClient.ReadStreamAsync(
            Direction.Backwards,
            aggregateId,
            StreamPosition.End,
            maxCount: 1,
            cancellationToken: cancellationToken);

        var state = await result.ReadState;

        if (state == ReadState.StreamNotFound)
        {
            return 0;
        }

        var events = await result.ToListAsync(cancellationToken);
        if (events.Count == 0)
        {
            return 0;
        }

        return (ulong)events[0].Event.EventNumber.ToInt64() + 1;
    }

    public async Task<IEnumerable<EventEnvelope>> ReadStreamAsync(string aggregateId, CancellationToken cancellationToken)
    {
        var result = eventStoreClient.ReadStreamAsync(
            Direction.Forwards,
            aggregateId,
            StreamPosition.Start,
            cancellationToken: cancellationToken);

        var state = await result.ReadState;

        if (state == ReadState.StreamNotFound)
        {
            return [];
        }

        var events = new List<EventEnvelope>();
        var streamType = ExtractStreamType(aggregateId);

        await foreach (var resolvedEvent in result)
        {
            var eventMetadata = JsonSerializer.Deserialize<EventMetadata>(
                Encoding.UTF8.GetString(resolvedEvent.Event.Metadata.Span),
                serializerOptions);

            if (eventMetadata?.EventTypeName == null)
            {
                continue;
            }

            var eventType = Type.GetType(eventMetadata.EventTypeName);
            if (eventType == null)
            {
                continue;
            }

            var eventData = JsonSerializer.Deserialize(
                Encoding.UTF8.GetString(resolvedEvent.Event.Data.Span),
                eventType,
                serializerOptions);

            if (eventData == null)
            {
                continue;
            }

            var envelope = new EventEnvelope(
                @event: eventData,
                streamType: streamType,
                streamId: aggregateId,
                streamVersion: (ulong)resolvedEvent.Event.EventNumber.ToInt64() + 1,
                timestamp: resolvedEvent.Event.Created,
                eventTypeName: resolvedEvent.Event.EventType);

            events.Add(envelope);
        }

        return events;
    }

    public async Task<bool> StreamExistsAsync(string aggregateId, CancellationToken cancellationToken)
    {
        var result = eventStoreClient.ReadStreamAsync(
            Direction.Forwards,
            aggregateId,
            StreamPosition.Start,
            maxCount: 1,
            cancellationToken: cancellationToken);

        var state = await result.ReadState;

        return state != ReadState.StreamNotFound;
    }

    private static string ExtractStreamType(string aggregateId)
    {
        var parts = aggregateId.Split('-', 2);
        return parts.Length > 0 ? parts[0] : "Unknown";
    }

    private sealed class EventMetadata(string eventTypeName)
    {
        public string EventTypeName { get; } = eventTypeName;
    }
}
