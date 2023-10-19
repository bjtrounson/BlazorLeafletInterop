using BlazorLeafletInterop.Models.Options.Base;

namespace BlazorLeafletInterop.Models.Options.Layer.Vector;

public class PathOptions : InteractiveLayerOptions
{
    public bool Stroke { get; set; } = true;
    public string Color { get; set; } = "#3388ff";
    public double Weight { get; set; } = 3;
    public double Opacity { get; set; } = 1.0;
    public string LineCap { get; set; } = "round";
    public string LineJoin { get; set; } = "round";
    public string? DashArray { get; set; } = null;
    public string? DashOffset { get; set; } = null;
    public bool? Fill { get; set; } = null;
    public string FillColor { get; set; } = "#3388ff";
    public double FillOpacity { get; set; } = 0.2;
    public string FillRule { get; set; } = "evenodd";
    public override bool BubblingMouseEvents { get; set; } = true;
    public string? ClassName { get; set; } = null;
}