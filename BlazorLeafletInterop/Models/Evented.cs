using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace BlazorLeafletInterop.Models;

public partial class Evented
{
    public static partial class Interop
    {
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonTypeInfo))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonSerializerContext))]
        static Interop() {}
        
        [JSImport("on", "BlazorLeafletInterop/Evented")]
        public static partial JSObject On(string eventName, [JSMarshalAs<JSType.Function<JSType.Object>>] Action<JSObject> callback);

        [JSImport("on", "BlazorLeafletInterop/Evented")]
        public static partial JSObject On(JSObject eventMap);
        
        [JSImport("off", "BlazorLeafletInterop/Evented")]
        public static partial JSObject Off(string eventName, [JSMarshalAs<JSType.Function<JSType.Object>>] Action<JSObject> callback);
        
        [JSImport("off", "BlazorLeafletInterop/Evented")]
        public static partial JSObject Off(JSObject eventMap);
        
        [JSImport("off", "BlazorLeafletInterop/Evented")]
        public static partial JSObject Off();
        
        [JSImport("fire", "BlazorLeafletInterop/Evented")]
        public static partial JSObject Fire(string eventName, JSObject? data, bool? propagate);
        
        [JSImport("listens", "BlazorLeafletInterop/Evented")]
        public static partial bool Listens(string eventName, bool? propagate);
        
        [JSImport("once", "BlazorLeafletInterop/Evented")]
        public static partial JSObject Once(string eventName, [JSMarshalAs<JSType.Function<JSType.Object>>] Action<JSObject> callback);
        
        [JSImport("once", "BlazorLeafletInterop/Evented")]
        public static partial JSObject Once(JSObject eventMap);
        
        [JSImport("addEventParent", "BlazorLeafletInterop/Evented")]
        public static partial JSObject AddEventParent(JSObject parent);
        
        [JSImport("removeEventParent", "BlazorLeafletInterop/Evented")]
        public static partial JSObject RemoveEventParent(JSObject parent);
        
        [JSImport("addEventListener", "BlazorLeafletInterop/Evented")]
        public static partial JSObject AddEventListener(string eventName, [JSMarshalAs<JSType.Function<JSType.Object>>] Action<JSObject> callback);
        
        [JSImport("addEventListener", "BlazorLeafletInterop/Evented")]
        public static partial JSObject AddEventListener(JSObject eventMap);
        
        [JSImport("removeEventListener", "BlazorLeafletInterop/Evented")]
        public static partial JSObject RemoveEventListener(string eventName, [JSMarshalAs<JSType.Function<JSType.Object>>] Action<JSObject> callback);
        
        [JSImport("removeEventListener", "BlazorLeafletInterop/Evented")]
        public static partial JSObject RemoveEventListener(JSObject eventMap);
        
        [JSImport("removeEventListener", "BlazorLeafletInterop/Evented")]
        public static partial JSObject RemoveEventListener();
        
        [JSImport("clearAllEventListeners", "BlazorLeafletInterop/Evented")]
        public static partial JSObject ClearAllEventListeners();
        
        [JSImport("addOneTimeEventListener", "BlazorLeafletInterop/Evented")]
        public static partial JSObject AddOneTimeEventListener(string eventName, [JSMarshalAs<JSType.Function<JSType.Object>>] Action<JSObject> callback);
        
        [JSImport("addOneTimeEventListener", "BlazorLeafletInterop/Evented")]
        public static partial JSObject AddOneTimeEventListener(JSObject eventMap);
        
        [JSImport("fireEvent", "BlazorLeafletInterop/Evented")]
        public static partial JSObject FireEvent(string type, JSObject? data);
        
        [JSImport("hasEventListeners", "BlazorLeafletInterop/Evented")]
        public static partial bool HasEventListeners(string type);
    }
}