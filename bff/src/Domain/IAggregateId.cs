namespace ProjectFollowUp.BFF.Domain;

public interface IAggregateId<T>
    where T: IAggregateId<T>
{
    Guid ToGuid();

    static abstract T FromGuid(Guid guid);
}
