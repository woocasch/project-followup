namespace ProjectFollowUp.BFF.Domain.Project;

using System;
using System.Diagnostics.CodeAnalysis;


public readonly struct ProjectId : IAggregateId<ProjectId>
{
    private ProjectId(Guid value)
    {
        this.Value = value;
    }

    public readonly Guid Value { get; }

    public static ProjectId FromGuid(Guid guid) => new(guid);

    public static ProjectId NewId() => FromGuid(Guid.NewGuid());

    public Guid ToGuid() => this.Value;

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
