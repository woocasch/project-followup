namespace ProjectFollowUp.BFF.Infrastructure.ReadModel.Mongo;

using MongoDB.Driver;

using ProjectFollowUp.BFF.Application.Projects.ProjectionWorkers;
using ProjectFollowUp.BFF.Application.Projects.ReadModel;
using ProjectFollowUp.BFF.Domain.Project;
using ProjectFollowUp.BFF.Infrastructure.ReadModel.Mongo.ProjectProjection;

public sealed class ProjectProjectionWriter(
    ICollectionProvider collectionProvider) : ProjectionWriterBase<ProjectRecord, ProjectDto, ProjectId>, IProjectProjectionWriter
{
    protected override FilterDefinition<ProjectDto> ApplyFilterById(FilterDefinitionBuilder<ProjectDto> filterBuilder, ProjectId id)
    {
        return filterBuilder.Eq(dto => dto.Id, id.ToGuid());
    }

    protected override UpdateDefinition<ProjectDto> ApplyUpdateDefinition(UpdateDefinitionBuilder<ProjectDto> builder, ProjectRecord record)
    {
        return builder
            .Set(dto => dto.Title, record.Title)
            .Set(dto => dto.Description, record.Description)
            .Set(dto => dto.CreatedAt, record.CreatedAt);
    }

    protected override IMongoCollection<ProjectDto> GetCollection() => collectionProvider.Projects;

    protected override ProjectId GetIdFromRecord(ProjectRecord record) => record.Id;

    protected override ProjectRecord MapFromDto(ProjectDto dto)
    {
        return new(
            ProjectId.FromGuid(dto.Id),
            dto.Title,
            dto.Description,
            dto.CreatedAt);
    }

    protected override ProjectDto MapFromRecord(ProjectRecord record)
    {
        return new ProjectDto
        {
            Id = record.Id.ToGuid(),
            Title = record.Title,
            Description = record.Description,
            CreatedAt = record.CreatedAt,
        };
    }
}
