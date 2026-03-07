namespace ProjectFollowUp.BFF.RabbitMqSetup;

public sealed class SetupSettings
{
    public required string RabbitMqRestUrl { get; init; }

    public required string RabbitMqRestUsername { get; init; }

    public required string RabbitMqRestPassword { get; init; }
}
