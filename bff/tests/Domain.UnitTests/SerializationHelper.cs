namespace ProjectFollowUp.BFF.Domain.UnitTests;

using System.Text.Json;

public static class SerializationHelper
{
    private static Lazy<JsonSerializerOptions> serializerOptions = new(SerializerFactory);

    public static JsonSerializerOptions SerializerOptions => serializerOptions.Value;

    public static string Serialize<T>(this T instance)
    {
        return JsonSerializer.Serialize(instance, SerializerOptions);
    }

    public static T? Deserialize<T>(this string serializedValue)
    {
        return JsonSerializer.Deserialize<T>(serializedValue, SerializerOptions);
    }

    private static JsonSerializerOptions SerializerFactory()
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false,
        };
        return options;
    }
}
