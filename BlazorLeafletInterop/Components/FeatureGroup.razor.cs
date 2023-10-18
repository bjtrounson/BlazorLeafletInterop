using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Microsoft.AspNetCore.Components;

namespace BlazorLeafletInterop.Components;

public partial class FeatureGroup : IDisposable
{
    public static partial class FeatureGroupInterop
    {
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonTypeInfo))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonSerializerContext))]
        static FeatureGroupInterop() { }
        
        [JSImport("createFeatureGroup", "BlazorLeafletInterop")]
        public static partial JSObject CreateFeatureGroup([JSMarshalAs<JSType.Any>] object options);
        
        [JSImport("setStyle", "BlazorLeafletInterop")]
        public static partial JSObject SetStyle([JSMarshalAs<JSType.Any>] object featureGroup, [JSMarshalAs<JSType.Any>] object style);
        
        [JSImport("bringToFront", "BlazorLeafletInterop")]
        public static partial JSObject BringToFront([JSMarshalAs<JSType.Any>] object featureGroup);
        
        [JSImport("bringToBack", "BlazorLeafletInterop")]
        public static partial JSObject BringToBack([JSMarshalAs<JSType.Any>] object featureGroup);
        
        [JSImport("getBounds", "BlazorLeafletInterop")]
        public static partial JSObject GetBounds([JSMarshalAs<JSType.Any>] object featureGroup);
    }
}