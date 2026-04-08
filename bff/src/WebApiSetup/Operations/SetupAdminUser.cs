namespace ProjectFollowUp.BFF.WebApiSetup.Operations;

using System.Threading;
using System.Threading.Tasks;

using Keycloak.Net;
using Keycloak.Net.Models.RealmsAdmin;
using Keycloak.Net.Models.Users;

using Microsoft.Extensions.Options;

using ProjectFollowUp.BFF.Application.EventSourcing;
using ProjectFollowUp.BFF.Application.Users;
using ProjectFollowUp.BFF.Domain.User;
using ProjectFollowUp.BFF.WebApiSetup;

public sealed class SetupAdminUser(
    IReporter reporter,
    IReadModel readModel,
    KeycloakClient keycloakClient,
    IEventStreamsRepository eventsRepository,
    IOptions<SetupSettings> settings) : IOperation
{
    public int Order => 2;

    public string Description => "Setup of admin user";

    private SetupSettings Settings => settings.Value;

    public async Task Execute(CancellationToken cancellationToken)
    {
        reporter.Info("Creating admin user information");
        var user = await this.GetUser(
            Settings.AdminUser.Username,
            cancellationToken);
        var aggregate = CreateAggregate(user);
        await eventsRepository.StoreStreamAsync(
            aggregate,
            cancellationToken);
        await this.SetUserIdInKeycloak(Settings.ApplicationRealm, user.Email, aggregate.AggregateId, cancellationToken);
    }

    public async Task<bool> IsNeeded(CancellationToken cancellationToken)
    {
        var user = await readModel.GetByEmail(Settings.AdminUser.Email, cancellationToken);
        return user is null;
    }

    private static UserAggregateRoot CreateAggregate(User user)
    {
        var aggregateId = UserId.FromGuid(Guid.NewGuid());
        var email = EmailAddress.FromString(user.Email);
        var displayName = user.FirstName + " " + user.LastName;
        if (string.IsNullOrWhiteSpace(displayName))
        {
            displayName = "ProjectFollowUp Admin";
        }

        var aggregate = UserAggregateRoot.Create(
            aggregateId,
            Guid.Parse(user.Id),
            displayName,
            email,
            DateTimeOffset.UtcNow);
        return aggregate;
    }

    private async Task<User> GetUser(string username, CancellationToken cancellationToken)
    {
        var users = (await keycloakClient.GetUsersAsync(
            Settings.ApplicationRealm,
            username: username,
            cancellationToken: cancellationToken)).ToList();
        if (users.Count == 0)
        {
            throw new InvalidOperationException($"User with username '{username}' not found.");
        }

        var user = users.First();
        return user;
    }

    private async Task SetUserIdInKeycloak(string realmId, string email, Guid userId, CancellationToken cancellationToken)
    {
        var users = (await keycloakClient.GetUsersAsync(
            realmId,
            email: email,
            cancellationToken: cancellationToken)).ToList();
        if (users.Count == 0)
        {
            throw new InvalidOperationException($"User with email '{email}' not found.");
        }
        var user = users.First();
        var mergedAttributes = new Dictionary<string, IEnumerable<string>>(
            user.Attributes ?? new Dictionary<string, IEnumerable<string>>())
        {
            ["projectfollowup-userid"] = [userId.ToString()]
        };
        var updatedUser = new User
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Enabled = user.Enabled,
            Attributes = mergedAttributes
        };
        await keycloakClient.UpdateUserAsync(
            realmId,
            user.Id,
            updatedUser,
            cancellationToken);
    }
}
