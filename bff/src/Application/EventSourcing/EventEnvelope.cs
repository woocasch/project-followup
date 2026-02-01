namespace ProjectFollowUp.BFF.Application.EventSourcing;

using ProjectFollowUp.BFF.Domain;

public readonly record struct EventEnvelope(
    IAggregateEvent Event,
    string StreamType,
    string StreamId,
    ulong StreamVersion,
    DateTimeOffset Timestamp,
    string EventTypeName);

