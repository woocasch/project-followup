namespace ProjectFollowUp.BFF.Application.ActivationLinks.ProjectionWorkers;

using ProjectFollowUp.BFF.Application.ActivationLinks.ReadModel;
using ProjectFollowUp.BFF.Application.EventSourcing;
using ProjectFollowUp.BFF.Domain.ActivationLink;

public interface IActivationLinkProjectionWriter : IProjectionWriter<ActivationLinkRecord, ActivationLinkId>
{
}
