namespace ProjectFollowUp.BFF.WebApiSetup;

public sealed class SetupSettings
{
    public required string MasterAdminUsername { get; init; }

    public required string MasterAdminPassword { get; init; }

    public required string MasterRealm { get; init; }

    public required string ApplicationRealm { get; init; }
}
