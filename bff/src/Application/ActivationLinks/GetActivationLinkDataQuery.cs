namespace ProjectFollowUp.BFF.Application.ActivationLinks;

using ProjectFollowUp.BFF.Application.Cqrs;
using ProjectFollowUp.BFF.Domain.ActivationLink;

public sealed class GetActivationLinkDataQuery(
    ActivationLinkId linkId) : IQuery<GetActivationLinkDataResult>
{
    public ActivationLinkId LinkId { get; } = linkId;
}
