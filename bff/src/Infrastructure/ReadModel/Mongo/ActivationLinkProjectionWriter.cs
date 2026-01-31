namespace ProjectFollowUp.BFF.Infrastructure.ReadModel.Mongo;

using MongoDB.Driver;

using ProjectFollowUp.BFF.Application.ActivationLinks.ProjectionWorkers;
using ProjectFollowUp.BFF.Application.ActivationLinks.ReadModel;
using ProjectFollowUp.BFF.Domain.ActivationLink;
using ProjectFollowUp.BFF.Domain.User;
using ProjectFollowUp.BFF.Infrastructure.ReadModel.Mongo.ActivationLinkProjection;

public sealed class ActivationLinkProjectionWriter(
    ICollectionProvider collectionProvider) : ProjectionWriterBase<ActivationLinkRecord, ActivationLinkDto, ActivationLinkId>, IActivationLinkProjectionWriter
{
    protected override FilterDefinition<ActivationLinkDto> ApplyFilterById(FilterDefinitionBuilder<ActivationLinkDto> filterBuilder, ActivationLinkId id)
    {
        return filterBuilder.Eq(l => l.Id, id.ToGuid());
    }

    protected override UpdateDefinition<ActivationLinkDto> ApplyUpdateDefinition(UpdateDefinitionBuilder<ActivationLinkDto> builder, ActivationLinkRecord record)
    {
        return builder
            .Set(l => l.UserId, record.UserId.ToGuid())
            .Set(l => l.LinkCode, record.LinkCode);
    }

    protected override IMongoCollection<ActivationLinkDto> GetCollection() => collectionProvider.ActivationLinks;

    protected override ActivationLinkId GetIdFromRecord(ActivationLinkRecord record) => record.LinkId;

    protected override ActivationLinkRecord MapFromDto(ActivationLinkDto dto)
    {
        return new(
            LinkId: ActivationLinkId.FromGuid(dto.Id),
            UserId: UserId.FromGuid(dto.UserId),
            LinkCode: dto.LinkCode);
    }

    protected override ActivationLinkDto MapFromRecord(ActivationLinkRecord record)
    {
        return new ActivationLinkDto
        {
            Id = record.LinkId.ToGuid(),
            UserId = record.UserId.ToGuid(),
            LinkCode = record.LinkCode,
        };
    }
}
