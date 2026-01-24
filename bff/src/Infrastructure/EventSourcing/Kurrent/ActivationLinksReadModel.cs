namespace ProjectFollowUp.BFF.Infrastructure.EventSourcing.Kurrent;

using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;


using KurrentDB.Client;

using ProjectFollowUp.BFF.Application.ActivationLinks;
using ProjectFollowUp.BFF.Application.ActivationLinks.ReadModel;

public sealed class ActivationLinksReadModel(
    KurrentDBClient client) : IReadModel
{
    public async Task<ActivationLinkData?> GetAsync(string linkCode, CancellationToken cancellationToken)
    {
        var searchStreamName = $"ActivationLinkCode-{linkCode}";
        try
        {
            var result = client.ReadStreamAsync(
                Direction.Backwards,
                searchStreamName,
                StreamPosition.End,
                maxCount: 1,
                resolveLinkTos: true,
                cancellationToken: cancellationToken);
 
            await foreach (var evt in result)
            {
                var json = Encoding.UTF8.GetString(evt.Event.Data.Span);
                return DeserializeActivationLink(json);
            }
        }
        catch (StreamNotFoundException)
        {
            return null;
        }

        return null;
    }

    private static ActivationLinkData? DeserializeActivationLink(string json)
    {
        var internalData = JsonSerializer.Deserialize<InternalActivationLinkData>(json);
        if (internalData is null)
        {
            return null;
        }

        return new ActivationLinkData(
            internalData.LinkId.Value,
            internalData.UserId.Value,
            internalData.LinkCode);
    }

    private sealed class InternalActivationLinkData
    {
        [JsonPropertyName("linkId")]
        public IdContainer LinkId { get; set; } = default!;

        [JsonPropertyName("userId")]
        public IdContainer UserId { get; set; } = default!;

        [JsonPropertyName("linkCode")]
        public string LinkCode { get; set; } = default!;

        public class IdContainer
        {
            [JsonPropertyName("value")]
            public Guid Value { get; set; }
        }
    }
}
