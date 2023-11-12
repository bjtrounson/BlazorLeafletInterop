using System.Text.Json;
using System.Text.Json.Serialization;

namespace BlazorLeafletInterop.Interops;

public class LeafletInterop
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };
    public static string ObjectToJson(object obj)
    {
        return JsonSerializer.Serialize(obj, JsonSerializerOptions);
    }
    
    public static T? JsonToObject<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json, JsonSerializerOptions);
    }
}