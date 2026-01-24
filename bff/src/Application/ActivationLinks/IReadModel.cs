namespace ProjectFollowUp.BFF.Application.ActivationLinks;

using ProjectFollowUp.BFF.Application.ActivationLinks.ReadModel;

public interface IReadModel
{
    Task<ActivationLinkData?> GetAsync(
        string linkCode,
        CancellationToken cancellationToken);
}
