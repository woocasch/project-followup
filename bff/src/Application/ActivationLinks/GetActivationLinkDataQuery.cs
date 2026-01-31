namespace ProjectFollowUp.BFF.Application.ActivationLinks;

using ProjectFollowUp.BFF.Application.Cqrs;
using ProjectFollowUp.BFF.Domain.ActivationLink;

public sealed class GetActivationLinkDataQuery : IQuery<GetActivationLinkDataResult>
{
    private readonly SearchMode mode;

    private readonly ActivationLinkId? linkId;

    private readonly string? linkCode;

    private GetActivationLinkDataQuery(
        SearchMode mode,
        ActivationLinkId? linkId,
        string? linkCode)
    {
        this.mode = mode;
        this.linkId = linkId;
        this.linkCode = linkCode;
    }

    public SearchMode Mode => this.mode;

    public ActivationLinkId LinkId
    {
        get
        {
            if (this.Mode != SearchMode.ByLinkId || linkId is null)
            {
                throw new InvalidOperationException("LinkId is not available in the current search mode.");
            }

            return this.linkId.Value;
        }
    }

    public string LinkCode
    {
        get
        {
            if (this.Mode != SearchMode.ByLinkCode || linkCode is null)
            {
                throw new InvalidOperationException("LinkCode is not available in the current search mode.");
            }

            return this.linkCode;
        }
    }

    public static GetActivationLinkDataQuery ByLinkId(ActivationLinkId linkId) => new(SearchMode.ByLinkId, linkId, null);

    public static GetActivationLinkDataQuery ByLinkCode(string linkCode) => new(SearchMode.ByLinkCode, null, linkCode);

    public enum SearchMode
    {
        ByLinkId,
        ByLinkCode
    }
}
