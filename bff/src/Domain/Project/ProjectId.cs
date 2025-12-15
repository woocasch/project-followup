namespace ProjectFollowUp.BFF.Domain.Project;

using System;
using System.Diagnostics.CodeAnalysis;

public readonly struct ProjectId(Guid value) : IAggregateId<ProjectId>
{
    public readonly Guid Value => value;

    public static ProjectId FromGuid(Guid guid) => new(guid);

    public Guid ToGuid() => value;

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj is ProjectId other)
        {
            return this.Value.Equals(other.Value);
        }

        return base.Equals(obj);
    }

    public override int GetHashCode() => this.Value.GetHashCode();

    public override string ToString() => this.Value.ToString();

    public static bool operator ==(ProjectId left, ProjectId right) => left.Equals(right);

    public static bool operator !=(ProjectId left, ProjectId right) => !(left == right);
}
