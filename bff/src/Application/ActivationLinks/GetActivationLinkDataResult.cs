namespace ProjectFollowUp.BFF.Application.ActivationLinks;

public sealed class GetActivationLinkDataResult(
    string linkCode,
    string emailAddress,
    string displayName,
    bool isUsed)
{
    public string LinkCode { get; } = linkCode;

    public string EmailAddress { get; } = emailAddress;

    public string DisplayName { get; } = displayName;

    public bool IsUsed { get; } = isUsed;
}
