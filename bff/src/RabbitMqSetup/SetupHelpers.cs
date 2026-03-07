namespace ProjectFollowUp.BFF.RabbitMqSetup;

using System.Net.Http.Headers;
using System.Text;

using RabbitMQ.Client;

public static class SetupHelpers
{
    public static RabbitMqClient CreateRabbitRestClient(SetupSettings settings)
    {
        var rabbitRestClient = new HttpClient()
        {
            BaseAddress = new Uri(settings.RabbitMqRestUrl),
        };
        var byteArray = Encoding.ASCII.GetBytes(
            $"{settings.RabbitMqRestUsername}:{settings.RabbitMqRestPassword}");
        rabbitRestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        var rabbitMqClient = new RabbitMqClient(rabbitRestClient);
        return rabbitMqClient;
    }
}
