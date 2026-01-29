namespace ProjectFollowUp.BFF.Application.ActivationLinks;

using System.Threading;
using System.Threading.Tasks;

using ProjectFollowUp.BFF.Application.Cqrs;
using ProjectFollowUp.BFF.Application.EventSourcing;
using ProjectFollowUp.BFF.Domain.ActivationLink;
using ProjectFollowUp.BFF.Domain.User;

public sealed class GetActivationLinkDataQueryHandler(
    IEventStreamsRepository eventsRepository,
    IAggregateFactory aggregateFactory) : QueryHandlerBase<GetActivationLinkDataQuery, GetActivationLinkDataResult>
{
    protected override async Task<GetActivationLinkDataResult?> HandleQuery(GetActivationLinkDataQuery query, CancellationToken cancellationToken)
    {
        var activationLink = await this.GetActivationLink(query.LinkId, cancellationToken);
        if (activationLink is null)
        {
            return null;
        }

        var user = await this.GetUser(activationLink.UserId, cancellationToken);
        if (user is null)
        {
            return null;
        }

        var result = new GetActivationLinkDataResult(
            activationLink.LinkCode,
            user.Email.Value,
            user.DisplayName,
            activationLink.IsUsed);
        return result;
    }

    private async Task<ActivationLinkAggregateRoot?> GetActivationLink(ActivationLinkId linkId, CancellationToken cancellationToken)
    {
        var activationLinkEvents = await eventsRepository.ReadStreamAsync<ActivationLinkAggregateRoot>(
            linkId.ToGuid(),
            cancellationToken);
        var result = aggregateFactory.Create(activationLinkEvents, ActivationLinkAggregateRoot.Rehydrate);
        return result;
    }

    private async Task<UserAggregateRoot> GetUser(UserId userId, CancellationToken cancellationToken)
    {
        var userEvents = await eventsRepository.ReadStreamAsync<UserAggregateRoot>(
            userId.ToGuid(),
            cancellationToken);
        var user = aggregateFactory.Create(userEvents, UserAggregateRoot.Rehydrate);
        return user;
    }
}
