namespace ProjectFollowUp.BFF.WebApi.Controllers.Users.LinkCodesModels;

public readonly record struct GetByLinkCodeOutput(
    string EmailAddress,
    string DisplayName,
    bool IsUsed);