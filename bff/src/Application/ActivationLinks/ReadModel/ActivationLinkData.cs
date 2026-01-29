namespace ProjectFollowUp.BFF.Application.ActivationLinks.ReadModel;

public sealed class ActivationLinkData(
    Guid linkId,
    Guid userId,
    string linkCode)
{
    public Guid LinkId { get; } = linkId;

    public Guid UserId { get; } = userId;

    public string LinkCode { get; } = linkCode;
}
