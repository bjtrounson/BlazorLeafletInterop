using BlazorLeafletInterop.Models.Basics;
using BlazorLeafletInterop.Models.Options.Layer.Vector;
using Microsoft.AspNetCore.Components;

namespace BlazorLeafletInterop.Components.Layers.Vector;

public class MultiPolyline : Path
{
    [Parameter] public PolylineOptions PolylineOptions { get; set; } = new();
    [Parameter] public List<List<LatLng>> LatLngs { get; set; } = new();
    
    
}