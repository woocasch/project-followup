namespace ProjectFollowUp.BFF.Application.ActivationLinks;

using System.Threading;
using System.Threading.Tasks;

using ProjectFollowUp.BFF.Application.Cqrs;
using ProjectFollowUp.BFF.Application.EventSourcing;
using ProjectFollowUp.BFF.Domain.ActivationLink;
using ProjectFollowUp.BFF.Domain.User;

public sealed class GetActivationLinkDataQueryHandler(
    IEventStreamsRepository eventsRepository,
    IReadModel readModel,
    IAggregateFactory aggregateFactory) : QueryHandlerBase<GetActivationLinkDataQuery, GetActivationLinkDataResult>
{
    protected override async Task<GetActivationLinkDataResult?> HandleQuery(
        GetActivationLinkDataQuery query,
        CancellationToken cancellationToken)
    {
        var activationLink = await this.GetActivationLink(query, cancellationToken);
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

    private async Task<ActivationLinkAggregateRoot?> GetActivationLink(
        GetActivationLinkDataQuery query,
        CancellationToken cancellationToken)
    {
        var linkId = await this.GetActivationLinkId(query, cancellationToken);
        if (linkId is null)
        {
            return null;
        }

        var activationLinkEvents = await eventsRepository.ReadStreamAsync<ActivationLinkAggregateRoot>(
            linkId.Value.ToGuid(),
            cancellationToken);
        var result = aggregateFactory.Create(activationLinkEvents, ActivationLinkAggregateRoot.Rehydrate);
        return result;
    }

    private async Task<ActivationLinkId?> GetActivationLinkId(
        GetActivationLinkDataQuery query,
        CancellationToken cancellationToken)
    {
        if (query.Mode == GetActivationLinkDataQuery.SearchMode.ByLinkId)
        {
            return query.LinkId;
        }

        if (query.Mode == GetActivationLinkDataQuery.SearchMode.ByLinkCode)
        {
            var linkCode = query.LinkCode;
            var activationLink = await readModel.GetAsync(
                linkCode,
                cancellationToken);
            if (activationLink is null)
            {
                return null;
            }

            return activationLink.Value.LinkId;
        }

        throw new InvalidOperationException("Unsupported search mode.");
    }

    private async Task<UserAggregateRoot> GetUser(
        UserId userId,
        CancellationToken cancellationToken)
    {
        var userEvents = await eventsRepository.ReadStreamAsync<UserAggregateRoot>(
            userId.ToGuid(),
            cancellationToken);
        var user = aggregateFactory.Create(userEvents, UserAggregateRoot.Rehydrate);
        return user;
    }
}
