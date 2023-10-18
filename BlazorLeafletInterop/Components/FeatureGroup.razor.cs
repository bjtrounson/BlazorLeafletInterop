using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Microsoft.AspNetCore.Components;

namespace BlazorLeafletInterop.Components;

public partial class FeatureGroup
{
    public static partial class FeatureGroupInterop
    {
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonTypeInfo))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonSerializerContext))]
        static FeatureGroupInterop() { }
        
        [JSImport("createFeatureGroup", "BlazorLeafletInterop")]
        public static partial JSObject CreateFeatureGroup(JSObject options);
        
        [JSImport("setStyle", "BlazorLeafletInterop")]
        public static partial JSObject SetStyle(JSObject featureGroup, JSObject style);
        
        [JSImport("bringToFront", "BlazorLeafletInterop")]
        public static partial JSObject BringToFront(JSObject featureGroup);
        
        [JSImport("bringToBack", "BlazorLeafletInterop")]
        public static partial JSObject BringToBack(JSObject featureGroup);
        
        [JSImport("getBounds", "BlazorLeafletInterop")]
        public static partial JSObject GetBounds(JSObject featureGroup);
    }
}