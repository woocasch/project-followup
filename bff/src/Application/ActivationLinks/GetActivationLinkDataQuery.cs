namespace ProjectFollowUp.BFF.Application.ActivationLinks;

using ProjectFollowUp.BFF.Application.Cqrs;

public sealed class GetActivationLinkDataQuery(
    string linkCode) : IQuery<GetActivationLinkDataResult>
{
    public string LinkCode { get; } = linkCode;
}
