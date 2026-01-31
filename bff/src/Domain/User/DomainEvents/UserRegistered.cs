namespace ProjectFollowUp.BFF.Domain.User.DomainEvents;

public record struct UserRegistered(
    UserId UserId) : IDomainEvent;
