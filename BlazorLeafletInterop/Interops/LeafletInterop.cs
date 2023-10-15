using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace BlazorLeafletInterop.Interops;

[SupportedOSPlatform("browser")]
public static partial class LeafletInterop
{
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