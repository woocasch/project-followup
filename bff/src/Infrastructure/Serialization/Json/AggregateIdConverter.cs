namespace ProjectFollowUp.BFF.Infrastructure.Serialization.Json;

using System.Text.Json;
using System.Text.Json.Serialization;

using ProjectFollowUp.BFF.Domain;

public abstract class AggregateIdConverter<T> : JsonConverter<T>
    where T : IAggregateId<T>
{
    public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetGuid();
        return T.FromGuid(value);
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToGuid().ToString());
    }
}
