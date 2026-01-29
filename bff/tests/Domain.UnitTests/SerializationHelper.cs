namespace ProjectFollowUp.BFF.Domain.UnitTests;

using System.Text.Json;

using ProjectFollowUp.BFF.Infrastructure.Serialization.Json;

public static class SerializationHelper
{
    public static JsonSerializerOptions SerializerOptions => JsonSerializerOptionsFactory.GetOptions();

    public static string Serialize<T>(this T instance)
    {
        return JsonSerializer.Serialize(instance, SerializerOptions);
    }

    public static T? Deserialize<T>(this string serializedValue)
    {
        return JsonSerializer.Deserialize<T>(serializedValue, SerializerOptions);
    }
}
