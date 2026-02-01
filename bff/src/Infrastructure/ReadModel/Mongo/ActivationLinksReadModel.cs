namespace ProjectFollowUp.BFF.Infrastructure.ReadModel.Mongo;

using System.Threading;
using System.Threading.Tasks;

using MongoDB.Driver;

using ProjectFollowUp.BFF.Application.ActivationLinks;
using ProjectFollowUp.BFF.Application.ActivationLinks.ReadModel;
using ProjectFollowUp.BFF.Domain.ActivationLink;
using ProjectFollowUp.BFF.Domain.User;
using ProjectFollowUp.BFF.Infrastructure.ReadModel.Mongo.ActivationLinkProjection;

public sealed class ActivationLinksReadModel(
    ICollectionProvider collectionProvider) : IReadModel
{
    public async Task<ActivationLinkRecord?> GetAsync(string linkCode, CancellationToken cancellationToken)
    {
        var collection = collectionProvider.ActivationLinks;
        var filter = Builders<ActivationLinkDto>.Filter.Eq(link => link.LinkCode, linkCode);
        var searchOptions = new FindOptions<ActivationLinkDto>()
        {
            Limit = 1,
        };
        var itemsMatched = await collection.FindAsync(filter, searchOptions, cancellationToken)
            .ConfigureAwait(false);
        var items = await itemsMatched
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
        if (items.Count == 0)
        {
            return null;
        }

        var found = items[0];
        return new(
            LinkId: ActivationLinkId.FromGuid(found.Id),
            UserId: UserId.FromGuid(found.UserId),
            LinkCode: found.LinkCode);
    }
}
