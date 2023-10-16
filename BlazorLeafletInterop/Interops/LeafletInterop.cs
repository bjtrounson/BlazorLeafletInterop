using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace BlazorLeafletInterop.Interops;

[SupportedOSPlatform("browser")]
public static partial class LeafletInterop
{
    public static string ObjectToJson(object obj)
    {
        var serializerOptions = new JsonSerializerOptions
        {
            IgnoreNullValues = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
        };
        return JsonSerializer.Serialize(obj, serializerOptions);
    }
    public static JSObject JsonToObject(string json)
    {
        return Interop.JsonToObject(json);
    }
    
    [SupportedOSPlatform("browser")]
    public static partial class Interop
    {
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonTypeInfo))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonSerializerContext))]
        static Interop()
        {
        }

        [JSImport("jsonToObject", "BlazorLeafletInterop/LeafletInterop")]
        public static partial JSObject JsonToObject(string json);
    }
}