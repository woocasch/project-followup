namespace ProjectFollowUp.BFF.Domain.User;

public sealed class UserRegisteredEvent
{
    public required UserId UserId { get; set; }
}
