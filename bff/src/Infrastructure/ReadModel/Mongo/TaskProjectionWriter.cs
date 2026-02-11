namespace ProjectFollowUp.BFF.Infrastructure.ReadModel.Mongo;

using System;
using System.Threading;
using System.Threading.Tasks;

using MongoDB.Driver;

using ProjectFollowUp.BFF.Application.Tasks.ProjectionWorkers;
using ProjectFollowUp.BFF.Application.Tasks.ReadModel;
using ProjectFollowUp.BFF.Domain.Project;
using ProjectFollowUp.BFF.Infrastructure.ReadModel.Mongo.TaskProjection;

public sealed class TaskProjectionWriter(
    ICollectionProvider collectionProvider) : ProjectionWriterBase<TaskRecord, TaskDto, Guid>, ITaskProjectionWriter
{
    protected override FilterDefinition<TaskDto> ApplyFilterById(FilterDefinitionBuilder<TaskDto> filterBuilder, Guid id)
    {
        return filterBuilder.Eq(t => t.Id, id);
    }

    protected override UpdateDefinition<TaskDto> ApplyUpdateDefinition(UpdateDefinitionBuilder<TaskDto> builder, TaskRecord record)
    {
        return builder
            .Set(t => t.ProjectId, record.ProjectId.ToGuid())
            .Set(t => t.Title, record.Title)
            .Set(t => t.Description, record.Description)
            .Set(t => t.DueDate, record.DueDate)
            .Set(t => t.Status, (int)record.Status)
            .Set(t => t.CreatedAt, record.CreatedAt);
    }

    protected override IMongoCollection<TaskDto> GetCollection() => collectionProvider.Tasks;

    protected override Guid GetIdFromRecord(TaskRecord record) => record.Id;

    protected override TaskRecord MapFromDto(TaskDto dto)
    {
        return new(
            Id: dto.Id,
            ProjectId: ProjectId.FromGuid(dto.ProjectId),
            Title: dto.Title,
            Description: dto.Description,
            DueDate: dto.DueDate,
            Status: (ProjectTaskStatus)dto.Status,
            CreatedAt: dto.CreatedAt);
    }

    protected override TaskDto MapFromRecord(TaskRecord record)
    {
        return new TaskDto
        {
            Id = record.Id,
            ProjectId = record.ProjectId.ToGuid(),
            Title = record.Title,
            Description = record.Description,
            DueDate = record.DueDate,
            Status = (int)record.Status,
            CreatedAt = record.CreatedAt,
        };
    }
}
