namespace ProjectFollowUp.BFF.Application.Users;

using System.Collections.ObjectModel;

using ProjectFollowUp.BFF.Domain.Users;

internal static class UsersStore
{
    private static readonly Collection<(UserId UserId, object DomainEvent)> usersEvents = [];

    public static void AddEvent(UserId userId, object domainEvent)
    {
        usersEvents.Add((userId, domainEvent));
    }

    public static UserAggregateRoot? GetUserById(UserId userId)
    {
        var events = usersEvents
            .Where(pe => pe.UserId == userId)
            .Select(pe => pe.DomainEvent)
            .ToList();
        if (events.Count == 0)
        {
            return null;
        }

        var project = UserAggregateRoot.Rehydrate(events);
        return project;
    }

    public static IEnumerable<UserAggregateRoot> GetAllUsers()
    {
        var projectIds = usersEvents
            .Select(pe => pe.UserId)
            .Distinct()
            .ToList();
        return projectIds
            .Select(GetUserById)
            .Where(user => user != null)!;
    }
}
