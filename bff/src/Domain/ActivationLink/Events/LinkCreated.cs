namespace ProjectFollowUp.BFF.Domain.ActivationLink.Events;

using System.Text.Json.Serialization;

using ProjectFollowUp.BFF.Domain.User;

[method: JsonConstructor]
public readonly struct LinkCreated(
    ActivationLinkId linkId,
    UserId userId,
    string linkCode)
{
    public ActivationLinkId LinkId { get; } = linkId;

    public UserId UserId { get; } = userId;

    public string LinkCode { get; } = linkCode;
}
