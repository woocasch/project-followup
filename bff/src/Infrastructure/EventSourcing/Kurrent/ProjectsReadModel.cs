namespace ProjectFollowUp.BFF.Infrastructure.EventSourcing.Kurrent;

using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

using KurrentDB.Client;

using ProjectFollowUp.BFF.Application.Projects;
using ProjectFollowUp.BFF.Application.Projects.ReadModel;
using ProjectFollowUp.BFF.Domain.Project;
using ProjectFollowUp.BFF.Infrastructure.EventSourcing.Kurrent.ProjectionsProcessing;

public sealed class ProjectsReadModel(
    KurrentDBClient client,
    INamingService namingService) : IReadModel
{
    public async Task<ProjectData?> GetAsync(Guid projectId, CancellationToken cancellationToken)
    {
        var streamName = namingService.GetStreamName<ProjectAggregateRoot>(projectId);
        var projectionStreamName = $"$projections-{ProjectsProjection.ProjectionName}-{streamName}-result";

        try
        {
            var result = client.ReadStreamAsync(
                Direction.Backwards,
                projectionStreamName,
                StreamPosition.End,
                maxCount: 1,
                cancellationToken: cancellationToken);

            await foreach (var evt in result)
            {
                var json = Encoding.UTF8.GetString(evt.Event.Data.Span);
                return DeserializeProject(json);
            }
        }
        catch (StreamNotFoundException)
        {
            return null;
        }

        return null;
    }

    private static ProjectData? DeserializeProject(string json)
    {
        var internalData = JsonSerializer.Deserialize<InternalProjectData>(json);
        if (internalData is null)
        {
            return null;
        }

        return new ProjectData(
            internalData.Id,
            internalData.Title,
            internalData.Description);
    }

    private class InternalProjectData
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("createdAt")]
        public DateTimeOffset CreatedAt { get; set; }
    }

}
