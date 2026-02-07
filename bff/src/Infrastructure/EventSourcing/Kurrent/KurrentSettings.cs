namespace ProjectFollowUp.BFF.Infrastructure.EventSourcing.Kurrent;

public sealed class KurrentSettings
{
    public required string ConnectionString { get; init; }

    public required Dictionary<string, string> AggregateNameMappings { get; init; }

    public required ReadModelHydratorSettings ReadModelHydration { get; init; }

    public sealed class ReadModelHydratorSettings
    {
        public required string SubscriptionName { get; init; }

        public required int CheckpointAfterMs { get; init; }

        public required int CheckpointLowerBound { get; init; }

        public required int CheckpointUpperBound { get; init; }
    }
}
