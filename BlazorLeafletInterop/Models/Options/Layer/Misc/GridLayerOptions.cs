using BlazorLeafletInterop.Models.Basics;
using BlazorLeafletInterop.Models.Options.Base;

namespace BlazorLeafletInterop.Models.Options.Layer.Misc;

public class GridLayerOptions : LayerOptions
{
    public Point TileSize { get; set; } = new (256, 256);
    public double Opacity { get; set; } = 1.0;
    public bool UpdateWhenIdle { get; set; } = false;
    public bool UpdateWhenZooming { get; set; } = true;
    public int UpdateInterval { get; set; } = 200;
    public int ZIndex { get; set; } = 1;
    public LatLngBounds? Bounds { get; set; }
    public virtual double MinZoom { get; set; } = 0;
    public virtual double? MaxZoom { get; set; }
    public double? MaxNativeZoom { get; set; }
    public double? MinNativeZoom { get; set; }
    public bool NoWrap { get; set; } = false;
    public override string Pane { get; set; } = "tilePane";
    public string ClassName { get; set; } = "";
    public int KeepBuffer { get; set; } = 2;
}