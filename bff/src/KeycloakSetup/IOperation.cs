namespace ProjectFollowUp.BFF.KeycloakSetup;

public interface IOperation
{
    int Order { get; }

    string Description { get; }

    Task<bool> IsNeeded(CancellationToken cancellationToken);

    Task Execute(CancellationToken cancellationToken);
}
