namespace ProjectFollowUp.BFF.RabbitMqSetup;

using System.Net.Http.Json;

public sealed class RabbitMqClient(
    HttpClient httpClient) : IRabbitMqClient
{
    public async Task<bool> BindingExists(string vhost, string sourceName, IRabbitMqClient.BindingNode sourceType, string targetName, IRabbitMqClient.BindingNode targetType, CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(
            HttpMethod.Get,
            $"api/bindings/{Uri.EscapeDataString(vhost)}/{NodeTypeToUrlPart(sourceType)}/{Uri.EscapeDataString(sourceName)}/{NodeTypeToUrlPart(targetType)}/{Uri.EscapeDataString(targetName)}");
        var response = await httpClient.SendAsync(request, cancellationToken);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> CreateBinding(string vhost, string sourceName, IRabbitMqClient.BindingNode sourceType, string targetName, IRabbitMqClient.BindingNode targetType, CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(
            HttpMethod.Post,
            $"api/bindings/{Uri.EscapeDataString(vhost)}/{NodeTypeToUrlPart(sourceType)}/{Uri.EscapeDataString(sourceName)}/{NodeTypeToUrlPart(targetType)}/{Uri.EscapeDataString(targetName)}")
        {
            Content = JsonContent.Create(new
            {
                routing_key = string.Empty,
                arguments = new { },
            }),
        };
        var response = await httpClient.SendAsync(request, cancellationToken);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> CreateExchange(string vHost, string name, string type, bool durable, bool autodelete, CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, $"api/exchanges/{Uri.EscapeDataString(vHost)}/{Uri.EscapeDataString(name)}")
        {
            Content = JsonContent.Create(new
            {
                type,
                durable,
                autodelete,
            }),
        };
        var response = await httpClient.SendAsync(request, cancellationToken);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> CreateQueue(string vHost, string name, string type, bool durable, bool autodelete, CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, $"api/queues/{Uri.EscapeDataString(vHost)}/{Uri.EscapeDataString(name)}")
        {
            Content = JsonContent.Create(new
            {
                type,
                durable,
                autodelete,
            }),
        };
        var response = await httpClient.SendAsync(request, cancellationToken);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> CreateUser(string userName, string password, CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, $"api/users/{Uri.EscapeDataString(userName)}")
        {
            Content = JsonContent.Create(new
            {
                password,
                tags = "administrator",
            }),
        };
        var response = await httpClient.SendAsync(request, cancellationToken);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> CreateVirtualHost(string vhost, CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, $"api/vhosts/{Uri.EscapeDataString(vhost)}");
        var response = await httpClient.SendAsync(request, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync(cancellationToken: cancellationToken);
            Console.WriteLine($"Response code: {response.StatusCode}");
            Console.WriteLine($"Response content: {responseContent}");
        }

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> ExchangeExists(string vHost, string name, CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"api/exchanges/{Uri.EscapeDataString(vHost)}/{Uri.EscapeDataString(name)}");
        var response = await httpClient.SendAsync(request, cancellationToken);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> PermissionsAreSet(string userName, string vhost, CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"api/permissions/{Uri.EscapeDataString(vhost)}/{Uri.EscapeDataString(userName)}");
        var response = await httpClient.SendAsync(request, cancellationToken);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> QueueExists(string vHost, string name, CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"api/queues/{Uri.EscapeDataString(vHost)}/{Uri.EscapeDataString(name)}");
        var response = await httpClient.SendAsync(request, cancellationToken);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> SetPermissions(string userName, string vhost, string configurePermission, string writePermission, string readPermission, CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, $"api/permissions/{Uri.EscapeDataString(vhost)}/{Uri.EscapeDataString(userName)}")
        {
            Content = JsonContent.Create(new
            {
                configure = configurePermission,
                write = writePermission,
                read = readPermission,
            }),
        };
        var response = await httpClient.SendAsync(request, cancellationToken);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UserExists(string userName, CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"api/users/{Uri.EscapeDataString(userName)}");
        var response = await httpClient.SendAsync(request, cancellationToken);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> VirtualHostExists(string vhost, CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"api/vhosts/{Uri.EscapeDataString(vhost)}");
        var response = await httpClient.SendAsync(request, cancellationToken);
        return response.IsSuccessStatusCode;
    }

    private static string NodeTypeToUrlPart(IRabbitMqClient.BindingNode nodeType) => nodeType switch
    {
        IRabbitMqClient.BindingNode.Exchange => "e",
        IRabbitMqClient.BindingNode.Queue => "q",
        _ => throw new ArgumentOutOfRangeException(nameof(nodeType), $"Unexpected node type: {nodeType}"),
    };
}
