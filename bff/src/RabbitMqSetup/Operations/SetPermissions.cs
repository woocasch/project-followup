namespace ProjectFollowUp.BFF.RabbitMqSetup.Operations;

using System.Collections.ObjectModel;

public sealed class SetPermissions(
    IRabbitMqClient rabbitMqClient,
    IReporter reporter)
    : IOperation
{
    private readonly ReadOnlyCollection<PermissionMapping> requiredUsers = new([
        new("api-bff", "projectfollowup", ".*", ".*", ".*"),
        new("admin", "projectfollowup", ".*", ".*", ".*"),
    ]);

    public int Order => 3;

    public string Description => "Set users' permissions";

    public async Task Execute(CancellationToken cancellationToken)
    {
        reporter.Info("Setting users' permissions...");
        foreach (var permissionMapping in this.requiredUsers)
        {
            var set = await rabbitMqClient.SetPermissions(
                permissionMapping.UserName,
                permissionMapping.VHost,
                permissionMapping.ConfigurePermission,
                permissionMapping.WritePermission,
                permissionMapping.ReadPermission,
                cancellationToken);
            if (set)
            {
                reporter.Info($"Permissions for user '{permissionMapping.UserName}' set.");
            }
            else
            {
                reporter.Error($"Failed to set permissions for user '{permissionMapping.UserName}'.");
                throw new InvalidOperationException($"Failed to set permissions for user '{permissionMapping.UserName}'.");
            }
        }
    }

    public async Task<bool> IsNeeded(CancellationToken cancellationToken)
    {
        foreach (var permissionMapping in this.requiredUsers)
        {
            if (!await rabbitMqClient.PermissionsAreSet(
                permissionMapping.UserName,
                permissionMapping.VHost,
                cancellationToken))
            {
                return true;
            }
        }
        return false;
    }

    private record struct PermissionMapping(
        string UserName,
        string VHost,
        string ConfigurePermission,
        string WritePermission,
        string ReadPermission);
}
