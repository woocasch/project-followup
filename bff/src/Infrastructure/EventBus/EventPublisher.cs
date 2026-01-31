namespace ProjectFollowUp.BFF.Infrastructure.EventBus;

using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using ProjectFollowUp.BFF.Application.EventsBus;
using ProjectFollowUp.BFF.Domain;
using ProjectFollowUp.BFF.Infrastructure.Serialization.Json;

using RabbitMQ.Client;

public sealed class EventPublisher(
    IConnectionFactory connectionFactory,
    INamesMappings eventToExchangeMapper) : IEventPublisher
{
    public async Task Publish(IDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        using var connection = await connectionFactory.CreateConnectionAsync(cancellationToken);
        using var channel = await connection.CreateChannelAsync(cancellationToken: cancellationToken);
        var data = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(domainEvent, JsonSerializerOptionsFactory.GetOptions()));
        var exchangeName = eventToExchangeMapper.GetExchangeNameForEvent(domainEvent);
        if (string.IsNullOrWhiteSpace(exchangeName))
        {
            var messageFormat = EventPublisherResources.ExchangeNameNotFound;
            var message = string.Format(
                messageFormat,
                domainEvent.GetType().FullName);
            throw new InvalidOperationException(
                message);
        }

        var routingKey = string.Empty;
        await channel.BasicPublishAsync(exchangeName, routingKey, data, cancellationToken: cancellationToken);
    }
}
