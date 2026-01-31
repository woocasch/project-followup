namespace ProjectFollowUp.BFF.Infrastructure.ProjectionWriters.Mongo;

using System.Threading;
using System.Threading.Tasks;

using ProjectFollowUp.BFF.Application.Users.ProjectionWorkers;
using ProjectFollowUp.BFF.Application.Users.ReadModel;
using ProjectFollowUp.BFF.Domain.User;

public sealed class UserProjectionWriter : IUserProjectionWriter
{
    public async Task<UserRecord?> Get(UserId id, CancellationToken cancellationToken)
    {
        Console.WriteLine($"GETTING USER '{id}' FROM MONGO DB");
        await Task.Yield();
        return null;
    }

    public async Task Upsert(UserRecord user, CancellationToken cancellationToken)
    {
        Console.WriteLine($"UPSERTING USER '{user.Id}' TO MONGO DB");
        await Task.Yield();
    }
}
