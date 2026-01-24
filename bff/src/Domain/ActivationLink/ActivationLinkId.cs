namespace ProjectFollowUp.BFF.Domain.ActivationLink;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

[method: JsonConstructor]
public readonly struct ActivationLinkId(Guid value) : IAggregateId<ActivationLinkId>
{
    public readonly Guid Value => value;

    public static ActivationLinkId FromGuid(Guid guid) => new(guid);

    public static ActivationLinkId NewId() => FromGuid(Guid.NewGuid());

    public Guid ToGuid() => value;

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj is ActivationLinkId other)
        {
            return this.Value.Equals(other.Value);
        }

        return base.Equals(obj);
    }

    public override int GetHashCode() => this.Value.GetHashCode();

    public override string ToString() => this.Value.ToString();

    public static bool operator ==(ActivationLinkId left, ActivationLinkId right) => left.Equals(right);

    public static bool operator !=(ActivationLinkId left, ActivationLinkId right) => !(left == right);
}
