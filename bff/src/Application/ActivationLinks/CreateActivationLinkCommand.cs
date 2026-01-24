namespace ProjectFollowUp.BFF.Application.ActivationLinks;

using ProjectFollowUp.BFF.Application.Cqrs;
using ProjectFollowUp.BFF.Domain.User;

public sealed class CreateActivationLinkCommand : ICommand
{
    public CreateActivationLinkCommand(
        UserId userId,
        string linkCode)
    {
        this.UserId = userId;
        this.LinkCode = linkCode;
    }

    public UserId UserId { get; }

    public string LinkCode { get; }
}
