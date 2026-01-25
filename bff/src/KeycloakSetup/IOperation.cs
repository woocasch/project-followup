namespace ProjectFollowUp.BFF.KeycloakSetup;

public interface IOperation
{
    string Description { get; }

    Task<bool> IsNeeded(CancellationToken cancellationToken);

    Task Execute(CancellationToken cancellationToken);
}
