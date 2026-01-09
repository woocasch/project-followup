namespace ProjectFollowUp.BFF.Application.EventSourcing;

public readonly struct EventEnvelope(
    object @event,
    string streamType,
    string streamId,
    ulong streamVersion,
    DateTimeOffset timestamp,
    string eventTypeName)
{
    public object Event { get; } = @event;

    public string StreamType { get; } = streamType;

    public string StreamId { get; } = streamId;

    public ulong StreamVersion { get; } = streamVersion;

    public DateTimeOffset Timestamp { get; } = timestamp;

    public string EventTypeName { get; } = eventTypeName;
}

