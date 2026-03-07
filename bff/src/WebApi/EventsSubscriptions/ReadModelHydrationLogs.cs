namespace ProjectFollowUp.BFF.WebApi.EventsSubscriptions;

internal static partial class ReadModelHydrationLogs
{
    [LoggerMessage(
        EventId = EventIds.Starting,
        EventName = nameof(EventIds.Starting),
        Level = LogLevel.Information,
        Message = "Starting read model hydration process.")]
    public static partial void Starting(
        this ILogger<ReadModelHydration> logger);

    [LoggerMessage(
        EventId = EventIds.ConfigurationCompleted,
        EventName = nameof(EventIds.ConfigurationCompleted),
        Level = LogLevel.Information,
        Message = "Read model hydration configuration completed.")]
    public static partial void ConfigurationCompleted(
        this ILogger<ReadModelHydration> logger);

    [LoggerMessage(
        EventId = EventIds.ExitingSubscription,
        EventName = nameof(EventIds.ExitingSubscription),
        Level = LogLevel.Information,
        Message = "Exiting subscription.")]
    public static partial void ExitingSubscription(
        this ILogger<ReadModelHydration> logger);

    [LoggerMessage(
        EventId = EventIds.ClientAquired,
        EventName = nameof(EventIds.ClientAquired),
        Level = LogLevel.Debug,
        Message = "Persistent subscription client aquired.")]
    public static partial void ClientAquired(
        this ILogger<ReadModelHydration> logger);

    [LoggerMessage(
        EventId = EventIds.CheckingSubscriptionExists,
        EventName = nameof(EventIds.CheckingSubscriptionExists),
        Level = LogLevel.Information,
        Message = "Checking if persistent subscription already exists.")]

    public static partial void CheckingSubscriptionExists(
        this ILogger<ReadModelHydration> logger);

    [LoggerMessage(
        EventId = EventIds.CreatingSubscription,
        EventName = nameof(EventIds.CreatingSubscription),
        Level = LogLevel.Debug,
        Message = "Creating persistent subscription.")]
    public static partial void CreatingSubscription(
        this ILogger<ReadModelHydration> logger);

    [LoggerMessage(
        EventId = EventIds.UpdatingSubscription,
        EventName = nameof(EventIds.UpdatingSubscription),
        Level = LogLevel.Debug,
        Message = "Updating persistent subscription.")]
    public static partial void UpdatingSubscription(
        this ILogger<ReadModelHydration> logger);

    [LoggerMessage(
        EventId = EventIds.StartingHydrator,
        EventName = nameof(EventIds.StartingHydrator),
        Level = LogLevel.Information,
        Message = "Starting read model hydrator.")]
    public static partial void StartingHydrator(
        this ILogger<ReadModelHydration> logger);

    private static class EventIds
    {
        public const int Starting = 1;

        public const int ConfigurationCompleted = 2;

        public const int ExitingSubscription = 3;

        public const int ClientAquired = 4;

        public const int CheckingSubscriptionExists = 5;

        public const int CreatingSubscription = 6;

        public const int UpdatingSubscription = 7;

        public const int StartingHydrator = 8;
    }
}
