namespace ProjectFollowUp.BFF.WebApi.Security;

public sealed class SecuritySettings
{
    public bool RequireHttpsMetadata { get; set; }

    public CorsSettings Cors { get; set; } = new();
}

public sealed class CorsSettings
{
    public bool AllowAnyOrigin { get; set; }

    public string[] AllowedOrigins { get; set; } = [];
}
