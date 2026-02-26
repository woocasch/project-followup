namespace ProjectFollowUp.BFF.Infrastructure.ReadModel.Mongo;

using Microsoft.Extensions.Logging;

using MongoDB.Driver;

using ProjectFollowUp.BFF.Application.Users.ProjectionWorkers;
using ProjectFollowUp.BFF.Application.Users.ReadModel;
using ProjectFollowUp.BFF.Domain.User;
using ProjectFollowUp.BFF.Infrastructure.ReadModel.Mongo.UserProjection;

public sealed class UserProjectionWriter(
    ICollectionProvider collectionProvider,
    ILogger<UserProjectionWriter> logger)
    : ProjectionWriterBase<UserRecord, UserDto, UserId>(logger),
    IUserProjectionWriter
{
    protected override FilterDefinition<UserDto> ApplyFilterById(FilterDefinitionBuilder<UserDto> filterBuilder, UserId id)
    {
        return filterBuilder.Eq(dto => dto.Id, id.Value);
    }

    protected override UpdateDefinition<UserDto> ApplyUpdateDefinition(UpdateDefinitionBuilder<UserDto> builder, UserRecord record)
    {
        return builder
            .Set(u => u.CredentialsId, record.CredentialsId)
            .Set(u => u.Email, record.Email.Value)
            .Set(u => u.DisplayName, record.DisplayName)
            .Set(u => u.CreatedAt, record.CreatedAt);
    }

    protected override IMongoCollection<UserDto> GetCollection()
    {
        return collectionProvider.Users;
    }

    protected override UserRecord MapFromDto(UserDto dto)
    {
        return new(
            UserId.FromGuid(dto.Id),
            dto.CredentialsId,
            EmailAddress.FromString(dto.Email),
            dto.DisplayName,
            dto.CreatedAt);
    }

    protected override UserDto MapFromRecord(UserRecord record)
    {
        return new UserDto
        {
            Id = record.Id.Value,
            CredentialsId = record.CredentialsId,
            Email = record.Email.Value,
            DisplayName = record.DisplayName,
            CreatedAt = record.CreatedAt,
        };
    }

    protected override UserId GetIdFromRecord(UserRecord record) => record.Id;
}
