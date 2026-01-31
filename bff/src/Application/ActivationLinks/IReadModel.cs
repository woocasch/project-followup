namespace ProjectFollowUp.BFF.Application.ActivationLinks;

using ProjectFollowUp.BFF.Application.ActivationLinks.ReadModel;

public interface IReadModel
{
    Task<ActivationLinkRecord?> GetAsync(
        string linkCode,
        CancellationToken cancellationToken);
}
