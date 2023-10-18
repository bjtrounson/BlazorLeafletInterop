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
        await base.OnInitializedAsync();
    }

    public static partial class EventedInterop
    {
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonTypeInfo))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonSerializerContext))]
        static EventedInterop() {}
        
        [JSImport("on", "BlazorLeafletInterop")]
        public static partial JSObject On(JSObject layer, string eventName, [JSMarshalAs<JSType.Function<JSType.Object>>] Action<JSObject> callback);
        
        [JSImport("off", "BlazorLeafletInterop")]
        public static partial JSObject Off(JSObject layer, string eventName, [JSMarshalAs<JSType.Function<JSType.Object>>] Action<JSObject> callback);
        
        [JSImport("off", "BlazorLeafletInterop")]
        public static partial JSObject Off(JSObject layer);
        
        [JSImport("fire", "BlazorLeafletInterop")]
        public static partial JSObject Fire(JSObject layer, string eventName, JSObject? data, bool? propagate);
        
        [JSImport("listens", "BlazorLeafletInterop")]
        public static partial bool Listens(JSObject layer, string eventName, bool? propagate);
        
        [JSImport("once", "BlazorLeafletInterop")]
        public static partial JSObject Once(JSObject layer, string eventName, [JSMarshalAs<JSType.Function<JSType.Object>>] Action<JSObject> callback);
        
        [JSImport("addEventParent", "BlazorLeafletInterop")]
        public static partial JSObject AddEventParent(JSObject layer, JSObject parent);
        
        [JSImport("removeEventParent", "BlazorLeafletInterop")]
        public static partial JSObject RemoveEventParent(JSObject layer, JSObject parent);
        
        [JSImport("addEventListener", "BlazorLeafletInterop")]
        public static partial JSObject AddEventListener(JSObject layer, string eventName, [JSMarshalAs<JSType.Function<JSType.Object>>] Action<JSObject> callback);
        
        [JSImport("removeEventListener", "BlazorLeafletInterop")]
        public static partial JSObject RemoveEventListener(JSObject layer, string eventName, [JSMarshalAs<JSType.Function<JSType.Object>>] Action<JSObject> callback);
        
        [JSImport("removeEventListener", "BlazorLeafletInterop")]
        public static partial JSObject RemoveEventListener(JSObject layer);
        
        [JSImport("clearAllEventListeners", "BlazorLeafletInterop")]
        public static partial JSObject ClearAllEventListeners(JSObject layer);
        
        [JSImport("addOneTimeEventListener", "BlazorLeafletInterop")]
        public static partial JSObject AddOneTimeEventListener(JSObject layer, string eventName, [JSMarshalAs<JSType.Function<JSType.Object>>] Action<JSObject> callback);
        
        [JSImport("fireEvent", "BlazorLeafletInterop")]
        public static partial JSObject FireEvent(JSObject layer, string type, JSObject? data);
        
        [JSImport("hasEventListeners", "BlazorLeafletInterop")]
        public static partial bool HasEventListeners(JSObject layer, string type);
    }
}