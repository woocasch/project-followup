namespace ProjectFollowUp.BFF.Domain.ActivationLink.Events;

using ProjectFollowUp.BFF.Domain.User;

public record struct LinkCreated(
    ActivationLinkId LinkId,
    UserId UserId,
    string LinkCode): IEvent;
