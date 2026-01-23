namespace ProjectFollowUp.BFF.Application.Users;

public sealed class UserCreatedEvent
{
    public required Guid CredentialsId { get; set; }

    public required Guid ProfileId { get; set; }
}
