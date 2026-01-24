namespace ProjectFollowUp.BFF.Application.ActivationLinks;

using System.Threading;
using System.Threading.Tasks;

using ProjectFollowUp.BFF.Application.Cqrs;
using ProjectFollowUp.BFF.Application.EventSourcing;
using ProjectFollowUp.BFF.Domain.ActivationLink;
using ProjectFollowUp.BFF.Domain.User;

public sealed class GetActivationLinkDataQueryHandler(
    IReadModel readModel,
    IEventStreamsRepository eventsRepository,
    IAggregateFactory aggregateFactory) : QueryHandlerBase<GetActivationLinkDataQuery, GetActivationLinkDataResult>
{
    protected override async Task<GetActivationLinkDataResult?> HandleQuery(GetActivationLinkDataQuery query, CancellationToken cancellationToken)
    {
        var activationLink = await this.GetActivationLink(query.LinkCode, cancellationToken);
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
            query.LinkCode,
            user.Email.Value,
            user.DisplayName,
            activationLink.IsUsed);
        return result;
    }

    private async Task<ActivationLinkAggregateRoot?> GetActivationLink(string linkCode, CancellationToken cancellationToken)
    {
        var activationLink = await readModel.GetAsync(linkCode, cancellationToken);
        if (activationLink is null)
        {
            return null;
        }
        var activationLinkEvents = await eventsRepository.ReadStreamAsync<ActivationLinkAggregateRoot>(
            activationLink.LinkId,
            cancellationToken);
        var result = aggregateFactory.Create<ActivationLinkAggregateRoot>(activationLinkEvents, ActivationLinkAggregateRoot.Rehydrate);
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
