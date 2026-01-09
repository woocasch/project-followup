namespace ProjectFollowUp.BFF.Infrastructure.EventSourcing.Kurrent;

public sealed class KurrentSettings
{
    public required string ConnectionString { get; init; }

    public required Dictionary<string, string> AggregateNameMappings { get; init; }
}
