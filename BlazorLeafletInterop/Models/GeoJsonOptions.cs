﻿using System.Runtime.InteropServices.JavaScript;

namespace BlazorLeafletInterop.Models;

public class GeoJsonOptions : InteractiveLayerOptions
{
    public Action<object, object>? OnEachFeature { get; set; } = null;
    public Func<object, object, object>? PointToLayer { get; set; } = null;
    public Action<object>? Style { get; set; } = null;
    public Action<object>? Filter { get; set; } = null;
    public bool MarkersInheritOptions { get; set; } = false;
}