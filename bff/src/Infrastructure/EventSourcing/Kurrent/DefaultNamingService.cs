namespace ProjectFollowUp.BFF.Infrastructure.EventSourcing.Kurrent;

using Microsoft.Extensions.Options;

public sealed class DefaultNamingService(
    IOptions<KurrentSettings> settings) : INamingService
{
    private readonly Dictionary<Type, string> aggregatePrefixes = [];

    private KurrentSettings Settings => settings.Value;

    public string GetStreamName<TAggregateType>(Guid aggregateId) where TAggregateType : class
    {
        var aggregateType = typeof(TAggregateType);
        if (!aggregatePrefixes.TryGetValue(aggregateType, out var prefix))
        {
            prefix = this.CreatePrefix(aggregateType);
            aggregatePrefixes[aggregateType] = prefix;
        }

        return $"{prefix}-{aggregateId}";
    }

    private static string GetMappingKey(Type aggregateType)
    {
        var assembly = aggregateType.Assembly.GetName().Name;
        var typeName = aggregateType.FullName;
        return $"{typeName}, {assembly}";
    }

    private string CreatePrefix(Type aggregateType)
    {
        var fromSettings = this.Settings.AggregateNameMappings.TryGetValue(GetMappingKey(aggregateType), out var mappedName)
            ? mappedName
            : null;
        if (fromSettings is null)
        {
            return aggregateType.Name;
        }

        return fromSettings;
    }
}
