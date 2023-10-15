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
public partial class Map
{
    [Parameter]
    public MapOptions Options { get; set; } = new();
    
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    public JSObject? MapRef { get; set; }
    public bool IsRendered { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        if (!OperatingSystem.IsBrowser()) throw new PlatformNotSupportedException();
        await base.OnInitializedAsync();
        await JSHost.ImportAsync("BlazorLeafletInterop/Map", "../_content/BlazorLeafletInterop/bundle.js");
        MapRef = CreateMap("map", Options);
        if (MapRef is not null) IsRendered = true;
    }

    private JSObject CreateMap(string id, MapOptions options)
    {
        var lowerCamelCaseOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            IgnoreNullValues = true,
        };
        var optionsJson = JsonSerializer.Serialize(options, lowerCamelCaseOptions);
        return Interop.CreateMap(id, LeafletInterop.JsonToObject(optionsJson));
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
}