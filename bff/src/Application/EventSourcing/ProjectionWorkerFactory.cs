namespace ProjectFollowUp.BFF.Application.EventSourcing;

using System;
using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;

using ProjectFollowUp.BFF.Domain;

public sealed class ProjectionWorkerFactory(
    IServiceProvider serviceProvider) : IProjectionWorkerFactory
{
    public IEnumerable<IProjectionWorker> Create(Type aggregateEventType)
    {
        if (!aggregateEventType.IsAssignableTo(typeof(IAggregateEvent)))
        {
            throw new ArgumentException(
                $"Type '{aggregateEventType.FullName}' is not an '{nameof(IAggregateEvent)}'.",
                nameof(aggregateEventType));
        }

        var key = aggregateEventType.AssemblyQualifiedName;
        return serviceProvider.GetKeyedServices<IProjectionWorker>(key!);
    }
}
