namespace ProjectFollowUp.BFF.Infrastructure.EventBus;

using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using ProjectFollowUp.BFF.Application.EventsBus;
using ProjectFollowUp.BFF.Domain;
using ProjectFollowUp.BFF.Infrastructure.Serialization.Json;

using RabbitMQ.Client;

public sealed class EventPublisher(
    IConnectionFactory connectionFactory,
    INamesMappings eventToExchangeMapper,
    ILogger<EventPublisher> logger)
    : IEventPublisher
{
    public async Task Publish(IDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.Started(domainEvent.GetType());
        using var connection = await connectionFactory.CreateConnectionAsync(cancellationToken);
        using var channel = await connection.CreateChannelAsync(cancellationToken: cancellationToken);
        var data = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(domainEvent, domainEvent.GetType(), JsonSerializerOptionsFactory.GetOptions()));
        var exchangeName = eventToExchangeMapper.GetExchangeNameForEvent(domainEvent);
        logger.DataPrepared(domainEvent.GetType());
        if (string.IsNullOrWhiteSpace(exchangeName))
        {
            logger.ExchangeNameNotConfigured(domainEvent.GetType());
            var messageFormat = EventPublisherResources.ExchangeNameNotFound;
            var message = string.Format(
                messageFormat,
                domainEvent.GetType().FullName);
            throw new InvalidOperationException(
                message);
        }

        var routingKey = string.Empty;
        logger.PublishingMessage(domainEvent.GetType(), exchangeName);
        await channel.BasicPublishAsync(exchangeName, routingKey, data, cancellationToken: cancellationToken);
        logger.Completed(domainEvent.GetType());
    }
}
