using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using BlazorLeafletInterop.Interops;
using BlazorLeafletInterop.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorLeafletInterop.Components;

[SupportedOSPlatform("browser")]
public partial class Map : IDisposable
{
    [Parameter]
    public MapOptions MapOptions { get; set; } = new();
    
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    
    [Parameter]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    [Parameter]
    public string? Class { get; set; }

    public JSObject? MapRef { get; set; }
    public bool IsRendered { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        if (!OperatingSystem.IsBrowser()) throw new PlatformNotSupportedException();
        await base.OnInitializedAsync();
        await JSHost.ImportAsync("BlazorLeafletInterop/Map", "../_content/BlazorLeafletInterop/bundle.js");
        MapRef = CreateMap("map", MapOptions);
        if (MapRef is not null) IsRendered = true;
    }

    private JSObject CreateMap(string id, MapOptions options)
    {
        var optionsJson = LeafletInterop.ObjectToJson(options);
        return Interop.CreateMap(id, LeafletInterop.JsonToJsObject(optionsJson));
    }

    [SupportedOSPlatform("browser")]
    public partial class Interop
    {
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonTypeInfo))]
        [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(JsonSerializerContext))]
        static Interop()
        {
        }
        
        [JSImport("createMap", "BlazorLeafletInterop/Map")]
        public static partial JSObject CreateMap(string id, JSObject options);
    }

    public void Dispose()
    {
        MapRef?.Dispose();
    }
}