namespace ProjectFollowUp.BFF.KeycloakSetup;

public class SetupSettings
{
    public required string KeycloakBaseUrl { get; init; }

    public required string MasterAdminUsername { get; init; }

    public required string MasterAdminPassword { get; init; }

    public required string MasterRealm { get; init; }

    public required ProjectFollowUpRealmSettings ProjectFollowUpRealm { get; init; }

    public class ProjectFollowUpRealmSettings
    {
        public required string RealmId { get; init; }

        public required string RealmName { get; init; }

        public required ClientSettings UIClient { get; init; }

        public required ClientSettings WebApiBffClient { get; init; }

        public required AdminDetails AdminUser { get; init; }

        public class ClientSettings
        {
            public required string ClientId { get; init; }

            public required string DisplayName { get; init; }

            public required string[] RedirectUrls { get; init; }

            public required string[] WebOrigins { get; init; }
        }

        public class AdminDetails 
        {
            public required string Username { get; init; }

            public required string Email { get; init; }

            public required string Password { get; init; }
        }
    }
}
