namespace ProjectFollowUp.BFF.Infrastructure.EventSourcing;

public readonly record struct EventMetadata(
    string EventTypeName);
