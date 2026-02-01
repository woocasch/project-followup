namespace ProjectFollowUp.BFF.Infrastructure.ReadModel.Mongo;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using MongoDB.Driver;

using ProjectFollowUp.BFF.Application.Projects;
using ProjectFollowUp.BFF.Application.Projects.ReadModel;
using ProjectFollowUp.BFF.Domain.Project;
using ProjectFollowUp.BFF.Infrastructure.ReadModel.Mongo.ProjectProjection;

public sealed class ProjectsReadModel(
    ICollectionProvider collectionProvider) : IReadModel
{
    public async Task<IEnumerable<ProjectListItem>> Fetch(CancellationToken cancellationToken)
    {
        var collection = collectionProvider.Projects;
        var itemsMatched = await collection.FindAsync(
            Builders<ProjectDto>.Filter.Empty,
            cancellationToken: cancellationToken)
            .ConfigureAwait(false);
        var items = await itemsMatched
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
        var results = new List<ProjectListItem>(items.Count);
        foreach (var item in items)
        {
            results.Add(new ProjectListItem
            {
                Id = ProjectId.FromGuid(item.Id),
                Title = item.Title,
                Description = item.Description,
            });
        }
        return results;
    }

    public async Task<ProjectRecord?> Get(ProjectId id, CancellationToken cancellationToken)
    {
        var collection = collectionProvider.Projects;
        var filter = Builders<ProjectDto>.Filter.Eq(p => p.Id, id.ToGuid());
        var searchOptions = new FindOptions<ProjectDto>()
        {
            Limit = 1,
        };
        var itemsMatched = await collection.FindAsync(filter, searchOptions, cancellationToken)
            .ConfigureAwait(false);
        var items = await itemsMatched
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
        if (items.Count == 0)
        {
            return null;
        }

        var found = items[0];
        return new(
            ProjectId.FromGuid(found.Id),
            found.Title,
            found.Description,
            found.CreatedAt);
    }
}
