namespace ProjectFollowUp.BFF.Application.ActivationLinks.ReadModel;

using ProjectFollowUp.BFF.Domain.ActivationLink;
using ProjectFollowUp.BFF.Domain.User;

public readonly record struct ActivationLinkRecord(
    ActivationLinkId LinkId,
    UserId UserId,
    string LinkCode);
