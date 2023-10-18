using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;
using BlazorLeafletInterop.Interops;

namespace BlazorLeafletInterop.Models;

[SupportedOSPlatform("browser")]
public class BaseOptions
{
    public object ToJsObject()
    {
        return LeafletInterop.JsonToJsObject(LeafletInterop.ObjectToJson(this));
    }
}