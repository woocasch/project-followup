namespace ProjectFollowUp.BFF.Application.ActivationLinks;

public sealed class GetActivationLinkDataResult
{
    private GetActivationLinkDataResult(LinkData? link)
    {
        this.Link = link;
    }

    public LinkData? Link { get; }

    public bool LinkFound => this.Link is not null;

    public static GetActivationLinkDataResult NotFound() => new(null);

    public static GetActivationLinkDataResult Found(LinkData link) => new(link);

    public class LinkData(
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
}