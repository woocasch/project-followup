namespace ProjectFollowUp.BFF.Domain.User;

using System;
using System.Diagnostics.CodeAnalysis;

public readonly struct UserId : IAggregateId<UserId>
{
    private UserId(Guid value)
    {
        this.Value = value;
    }

    public readonly Guid Value { get; }

    public static UserId FromGuid(Guid guid) => new(guid);

    public static UserId NewId() => FromGuid(Guid.NewGuid());

    public Guid ToGuid() => this.Value;

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj is UserId other)
        {
            return this.Value.Equals(other.Value);
        }

        return base.Equals(obj);
    }

    public override int GetHashCode() => this.Value.GetHashCode();

    public override string ToString() => this.Value.ToString();

    public static bool operator ==(UserId left, UserId right) => left.Equals(right);

    public static bool operator !=(UserId left, UserId right) => !(left == right);
}
