namespace ProjectFollowUp.BFF.WebApiSetup;

public sealed class SetupSettings
{
    public required string MasterAdminUsername { get; init; }

    public required string MasterAdminPassword { get; init; }

    public required string MasterRealm { get; init; }

    public required string ApplicationRealm { get; init; }

    public required AdminUserSettings AdminUser { get; init; }

    public sealed class AdminUserSettings
    {
        public required string Username { get; init; }

        public required string Email { get; init; }
    }
}
