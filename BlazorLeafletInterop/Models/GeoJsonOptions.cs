using System.Runtime.InteropServices.JavaScript;
using BlazorLeafletInterop.Components.Base;

namespace BlazorLeafletInterop.Models;

public class GeoJsonOptions : InteractiveLayer
{
    public Action<JSObject, JSObject>? OnEachFeature { get; set; } 
    public Action<JSObject, JSObject>? PointToLayer { get; set; }
    public Action<JSObject>? Style { get; set; }
    public Action<JSObject>? Filter { get; set; }
    public bool MarkersInheritOptions { get; set; } = false;
}