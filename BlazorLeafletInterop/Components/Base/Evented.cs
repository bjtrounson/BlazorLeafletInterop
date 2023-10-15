using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Microsoft.AspNetCore.Components;

namespace BlazorLeafletInterop.Components.Base;

[SupportedOSPlatform("browser")]
public partial class Evented : ComponentBase
{
    protected override async Task OnInitializedAsync()
    {
        if (!OperatingSystem.IsBrowser()) throw new PlatformNotSupportedException();
        await JSHost.ImportAsync("BlazorLeafletInterop/LeafletInterop", "../_content/BlazorLeafletInterop/bundle.js");
        await JSHost.ImportAsync("BlazorLeafletInterop/Evented", "../_content/BlazorLeafletInterop/bundle.js");
        await base.OnInitializedAsync();
    }

    public static partial class EventedInterop
    {
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonTypeInfo))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonSerializerContext))]
        static EventedInterop() {}
        
        [JSImport("on", "BlazorLeafletInterop/Evented")]
        public static partial JSObject On(JSObject layer, string eventName, [JSMarshalAs<JSType.Function<JSType.Object>>] Action<JSObject> callback);
        
        [JSImport("off", "BlazorLeafletInterop/Evented")]
        public static partial JSObject Off(JSObject layer, string eventName, [JSMarshalAs<JSType.Function<JSType.Object>>] Action<JSObject> callback);
        
        [JSImport("off", "BlazorLeafletInterop/Evented")]
        public static partial JSObject Off(JSObject layer);
        
        [JSImport("fire", "BlazorLeafletInterop/Evented")]
        public static partial JSObject Fire(JSObject layer, string eventName, JSObject? data, bool? propagate);
        
        [JSImport("listens", "BlazorLeafletInterop/Evented")]
        public static partial bool Listens(JSObject layer, string eventName, bool? propagate);
        
        [JSImport("once", "BlazorLeafletInterop/Evented")]
        public static partial JSObject Once(JSObject layer, string eventName, [JSMarshalAs<JSType.Function<JSType.Object>>] Action<JSObject> callback);
        
        [JSImport("addEventParent", "BlazorLeafletInterop/Evented")]
        public static partial JSObject AddEventParent(JSObject layer, JSObject parent);
        
        [JSImport("removeEventParent", "BlazorLeafletInterop/Evented")]
        public static partial JSObject RemoveEventParent(JSObject layer, JSObject parent);
        
        [JSImport("addEventListener", "BlazorLeafletInterop/Evented")]
        public static partial JSObject AddEventListener(JSObject layer, string eventName, [JSMarshalAs<JSType.Function<JSType.Object>>] Action<JSObject> callback);
        
        [JSImport("removeEventListener", "BlazorLeafletInterop/Evented")]
        public static partial JSObject RemoveEventListener(JSObject layer, string eventName, [JSMarshalAs<JSType.Function<JSType.Object>>] Action<JSObject> callback);
        
        [JSImport("removeEventListener", "BlazorLeafletInterop/Evented")]
        public static partial JSObject RemoveEventListener(JSObject layer);
        
        [JSImport("clearAllEventListeners", "BlazorLeafletInterop/Evented")]
        public static partial JSObject ClearAllEventListeners(JSObject layer);
        
        [JSImport("addOneTimeEventListener", "BlazorLeafletInterop/Evented")]
        public static partial JSObject AddOneTimeEventListener(JSObject layer, string eventName, [JSMarshalAs<JSType.Function<JSType.Object>>] Action<JSObject> callback);
        
        [JSImport("fireEvent", "BlazorLeafletInterop/Evented")]
        public static partial JSObject FireEvent(JSObject layer, string type, JSObject? data);
        
        [JSImport("hasEventListeners", "BlazorLeafletInterop/Evented")]
        public static partial bool HasEventListeners(JSObject layer, string type);
    }
}