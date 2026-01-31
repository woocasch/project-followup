namespace ProjectFollowUp.BFF.Infrastructure.ProjectionWriters.Mongo;

public sealed class MongoSettings
{
    public required string ConnectionString { get; init; }
    public required string DatabaseName { get; init; }
}
