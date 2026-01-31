namespace ProjectFollowUp.BFF.Infrastructure.EventSourcing.ReadModel.MongoDb;

using System;
using System.Threading;
using System.Threading.Tasks;

using ProjectFollowUp.BFF.Infrastructure.EventSourcing.ReadModel.User;

public sealed class MongoUserManager : IUserManager
{
    public async Task<UserRecord?> GetCurrent(Guid id, CancellationToken cancellationToken)
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
