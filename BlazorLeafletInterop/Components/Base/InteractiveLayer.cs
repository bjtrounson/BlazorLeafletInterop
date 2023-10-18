using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using BlazorLeafletInterop.Models;

namespace BlazorLeafletInterop.Components.Base;

[SupportedOSPlatform("browser")]
public partial class InteractiveLayer : Layer
{
    public InteractiveLayerOptions InteractiveLayerOptions { get; set; } = new();

    public static partial class InteractiveLayerInterop
    {
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonTypeInfo))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonSerializerContext))]
        static InteractiveLayerInterop() {}
    } 
}