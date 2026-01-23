namespace ProjectFollowUp.BFF.Infrastructure.EventBus.Users;

using System.Threading.Tasks;

using MassTransit;

using ProjectFollowUp.BFF.Application.Users;

public sealed class SendActivationEmailConsumer : IConsumer<UserCreatedEvent>
{
    public Task Consume(ConsumeContext<UserCreatedEvent> context)
    {
        Console.WriteLine("SendActivationEmailConsumer triggered");
        Console.WriteLine($"CredentialsId: {context.Message.CredentialsId}");
        Console.WriteLine($"ProfileId: {context.Message.ProfileId}");
        // Create activation link
        // Send email with activation link
        return Task.CompletedTask;
    }
}
