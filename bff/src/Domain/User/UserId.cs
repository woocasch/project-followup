namespace ProjectFollowUp.BFF.Domain.User;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

[method: JsonConstructor]
public readonly struct UserId(Guid value) : IAggregateId<UserId>
{
    public readonly Guid Value => value;

    public static UserId FromGuid(Guid guid) => new(guid);

    public Guid ToGuid() => value;

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
