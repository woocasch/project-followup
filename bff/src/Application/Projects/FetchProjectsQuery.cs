namespace ProjectFollowUp.BFF.Application.Projects;

using ProjectFollowUp.BFF.Application.Cqrs;

public sealed class FetchProjectsQuery : IQuery<FetchProjectsResult>
{
    public FetchProjectsQuery(Guid userId)
    {
        this.UserId = userId;
    }

    public Guid UserId { get; }
}
