using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BlazorLeafletInterop.Interops;

[SupportedOSPlatform("browser")]
public static partial class LeafletInterop
{
    public static string ObjectToJson(object obj)
    {
        var defaultContractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy(),
        };
        return JsonConvert.SerializeObject(obj, new JsonSerializerSettings()
        {
            ContractResolver = defaultContractResolver,
            NullValueHandling = NullValueHandling.Ignore
        });
    }
    public static JSObject JsonToJsObject(string json)
    {
        return Interop.JsonToObject(json);
    }
    
    public static T? JsonToObject<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }
    
    public static string GetElementInnerHtml(string elementId)
    {
        return Interop.GetElementInnerHtml(elementId);
    }
    
    [SupportedOSPlatform("browser")]
    private static partial class Interop
    {
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonTypeInfo))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonSerializerContext))]
        static Interop()
        {
        }

        [JSImport("jsonToObject", "BlazorLeafletInterop/LeafletInterop")]
        public static partial JSObject JsonToObject(string json);
        
        [JSImport("getElementInnerHtml", "BlazorLeafletInterop/LeafletInterop")]
        public static partial string GetElementInnerHtml(string elementId);
    }
}