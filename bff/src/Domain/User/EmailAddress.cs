namespace ProjectFollowUp.BFF.Domain.User;

using System.Diagnostics;
using System.Text.Json.Serialization;

[DebuggerDisplay("EmailAddress: {Value}")]
public sealed class EmailAddress : IEquatable<EmailAddress>
{
    [JsonConstructor]
    private EmailAddress(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static EmailAddress FromString(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException(EmailAddressResources.FromString_EmailEmpty, nameof(email));
        }

        var atCount = email.Count(c => c == '@');
        if (atCount != 1)
        {
            throw new ArgumentException(EmailAddressResources.FromString_MultipleAts, nameof(email));
        }

        var parts = email.Split('@');
        var accountName = parts[0];
        var domain = parts[1];

        if (string.IsNullOrWhiteSpace(accountName))
        {
            throw new ArgumentException(EmailAddressResources.FromString_AccountNameEmpty, nameof(email));
        }

        if (string.IsNullOrWhiteSpace(domain))
        {
            throw new ArgumentException(EmailAddressResources.FromString_DomainEmpty, nameof(email));
        }

        if (!char.IsLetterOrDigit(accountName[0]) || !char.IsLetterOrDigit(accountName[^1]))
        {
            throw new ArgumentException(EmailAddressResources.FromString_AccountNameInvalid, nameof(email));
        }

        if (!char.IsLetterOrDigit(domain[0]) || !char.IsLetterOrDigit(domain[^1]))
        {
            throw new ArgumentException(EmailAddressResources.FromString_DomainInvalid, nameof(email));
        }

        return new EmailAddress(email);
    }

    public bool Equals(EmailAddress? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        return obj is EmailAddress other && this.Equals(other);
    }

    public override int GetHashCode()
    {
        return this.Value.GetHashCode();
    }

    public static bool operator ==(EmailAddress? left, EmailAddress? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(EmailAddress? left, EmailAddress? right)
    {
        return !Equals(left, right);
    }

    public static implicit operator string(EmailAddress emailAddress)
    {
        return emailAddress.Value;
    }

    public static explicit operator EmailAddress(string email)
    {
        return FromString(email);
    }
}
