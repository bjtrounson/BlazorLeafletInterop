using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BlazorLeafletInterop.Interops;

public class LeafletInterop
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
    
    public static T? JsonToObject<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }
}