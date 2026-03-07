namespace ProjectFollowUp.BFF.RabbitMqSetup;

public interface IRabbitMqClient
{
    Task<bool> VirtualHostExists(string vhost, CancellationToken cancellationToken);

    Task<bool> CreateVirtualHost(string vhost, CancellationToken cancellationToken);

    Task<bool> UserExists(string userName, CancellationToken cancellationToken);

    Task<bool> CreateUser(string userName, string password, CancellationToken cancellationToken);

    Task<bool> PermissionsAreSet(string userName, string vhost, CancellationToken cancellationToken);

    Task<bool> SetPermissions(
        string userName,
        string vhost,
        string configurePermission,
        string writePermission,
        string readPermission,
        CancellationToken cancellationToken);
    Task<bool> ExchangeExists(string vHost, string name, CancellationToken cancellationToken);

    Task<bool> CreateExchange(string vHost, string name, string type, bool durable, bool autodelete, CancellationToken cancellationToken);

    Task<bool> QueueExists(string vHost, string name, CancellationToken cancellationToken);

    Task<bool> CreateQueue(string vHost, string name, string type, bool durable, bool autodelete, CancellationToken cancellationToken);

    Task<bool> BindingExists(string vhost, string sourceName, BindingNode sourceType, string targetName, BindingNode targetType, CancellationToken cancellationToken);

    Task<bool> CreateBinding(string vhost, string sourceName, BindingNode sourceType, string targetName, BindingNode targetType, CancellationToken cancellationToken);

    public enum BindingNode
    {
        Exchange,
        Queue
    }
}
