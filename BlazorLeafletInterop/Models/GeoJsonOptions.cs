using System.Runtime.InteropServices.JavaScript;
using BlazorLeafletInterop.Components.Base;

namespace BlazorLeafletInterop.Models;

public class GeoJsonOptions : InteractiveLayer
{
    public Action<JSObject, JSObject>? OnEachFeature { get; set; } = null;
    public Func<JSObject, JSObject, JSObject>? PointToLayer { get; set; } = null;
    public Action<JSObject>? Style { get; set; } = null;
    public Action<JSObject>? Filter { get; set; } = null;
    public bool MarkersInheritOptions { get; set; } = false;
}