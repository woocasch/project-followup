namespace ProjectFollowUp.BFF.RabbitMqSetup.Operations;

using System.Collections.ObjectModel;

public sealed class CreateBindings(
    IRabbitMqClient rabbitMqClient,
    IReporter reporter)
    : IOperation
{
    private readonly ReadOnlyCollection<Binding> bindings = new([
        new("projectfollowup", "EX.bff.user.registered", IRabbitMqClient.BindingNode.Exchange, "QU.bff.activation-link.user.registered", IRabbitMqClient.BindingNode.Queue),
        new("projectfollowup", "EX.bff.activation-link.generated", IRabbitMqClient.BindingNode.Exchange, "QU.bff.documents.activation-link-generated", IRabbitMqClient.BindingNode.Queue),
    ]);

    public int Order => 6;

    public string Description => "Creating bindings";

    public async Task Execute(CancellationToken cancellationToken)
    {
        reporter.Info("Creating bindings...");
        foreach (var binding in bindings)
        {
            if (await rabbitMqClient.BindingExists(
                binding.VHost,
                binding.Source,
                binding.SourceType,
                binding.Target,
                binding.TargetType,
                cancellationToken))
            {
                continue;
            }

            var created = await rabbitMqClient.CreateBinding(
                binding.VHost,
                binding.Source,
                binding.SourceType,
                binding.Target,
                binding.TargetType,
                cancellationToken);
            if (created)
            {
                reporter.Info($"Binding {binding.SourceType} '{binding.Source}' to {binding.TargetType} '{binding.Target}' on vhost '{binding.VHost}' created.");
            }
            else
            {
                reporter.Error($"Failed to create binding {binding.SourceType} '{binding.Source}' to {binding.TargetType} '{binding.Target}'.");
                throw new InvalidOperationException($"Failed to create binding {binding.SourceType} '{binding.Source}' to {binding.TargetType} '{binding.Target}'.");
            }
        }
    }

    public async Task<bool> IsNeeded(CancellationToken cancellationToken)
    {
        foreach (var binding in bindings)
        {
            if (!await rabbitMqClient.BindingExists(
                binding.VHost,
                binding.Source,
                binding.SourceType,
                binding.Target,
                binding.TargetType,
                cancellationToken))
            {
                return true;
            }
        }

        return false;
    }

    private record struct Binding(
        string VHost,
        string Source,
        IRabbitMqClient.BindingNode SourceType,
        string Target,
        IRabbitMqClient.BindingNode TargetType);
}
