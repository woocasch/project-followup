namespace ProjectFollowUp.BFF.Infrastructure.Serialization.Json.User;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

using ProjectFollowUp.BFF.Domain.User;

public sealed class EmailAddressConverter : JsonConverter<EmailAddress>
{
    public override EmailAddress? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        if (string.IsNullOrEmpty(value))
        {
            return null;
        }

        return EmailAddress.FromString(value);
    }

    public override void Write(Utf8JsonWriter writer, EmailAddress value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value);
    }
}
