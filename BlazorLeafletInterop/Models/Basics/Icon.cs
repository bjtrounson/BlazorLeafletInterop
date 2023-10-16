using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using BlazorLeafletInterop.Interops;

namespace BlazorLeafletInterop.Models.Basics;

[SupportedOSPlatform("browser")]
public partial class Icon
{
    public IconOptions? IconOptions { get; set; }
    
    public Icon() {}

    public Icon(IconOptions options)
    {
        IconOptions = options;
    }
    
    public JSObject? IconRef { get; set; }

    public static async Task Initialize()
    {
        if (!OperatingSystem.IsBrowser()) throw new PlatformNotSupportedException();
        await JSHost.ImportAsync("BlazorLeafletInterop/Icon", "../_content/BlazorLeafletInterop/bundle.js");
    }
    
    public static async Task<JSObject> CreateIcon(IconOptions iconOptions)
    {
        await Initialize();
        var jsonOptions = LeafletInterop.ObjectToJson(iconOptions);
        return IconInterop.CreateIcon(LeafletInterop.JsonToJsObject(jsonOptions));
    }
    
    public static async Task<JSObject> CreateDefaultIcon()
    {
        await Initialize();
        return IconInterop.CreateDefaultIcon();
    }
    
    public bool IsOptionsValid()
    {
        return IconOptions is not null || IconOptions != new IconOptions();
    }
    
    public static partial class IconInterop
    {
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonTypeInfo))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonSerializerContext))]
        static IconInterop() {}
        
        [JSImport("createIcon", "BlazorLeafletInterop/Icon")]
        public static partial JSObject CreateIcon(JSObject iconOptions);
        
        [JSImport("createDefaultIcon", "BlazorLeafletInterop/Icon")]
        public static partial JSObject CreateDefaultIcon();
    }
}