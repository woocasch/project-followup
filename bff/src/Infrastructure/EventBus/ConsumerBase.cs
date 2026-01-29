namespace ProjectFollowUp.BFF.Infrastructure.EventBus;

using System.Text;
using System.Text.Json;

using ProjectFollowUp.BFF.Infrastructure.Serialization.Json;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

public abstract class ConsumerBase<TEvent>(
    IChannel channel) : AsyncEventingBasicConsumer(channel)
    where TEvent : struct
{
    public async Task BindToQueue(string queueName, CancellationToken cancellationToken)
    {
        this.ReceivedAsync += OnReceivedAsync;
        await this.Channel.BasicConsumeAsync(
            queueName,
            true,
            this,
            cancellationToken);
    }

    public void UnbindFromQueue()
    {
        this.ReceivedAsync -= OnReceivedAsync;
    }

    public abstract Task Handle(TEvent @event, CancellationToken cancellationToken);

    private async Task OnReceivedAsync(object model, BasicDeliverEventArgs ea)
    {
        var bodyBytes = ea.Body.ToArray();
        var bodyString = Encoding.UTF8.GetString(bodyBytes);
        var @event = JsonSerializer.Deserialize<TEvent>(bodyString, JsonSerializerOptionsFactory.GetOptions());
        await this.Handle(@event, ea.CancellationToken);
    }
}
