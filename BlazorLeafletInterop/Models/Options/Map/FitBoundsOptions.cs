using System.Runtime.Versioning;
using BlazorLeafletInterop.Models.Basics;

namespace BlazorLeafletInterop.Models.Options.Map;

[SupportedOSPlatform("browser")]
public class FitBoundsOptions : BaseOptions
{
    public bool? Animate { get; set; } = null;
    public double Duration { get; set; } = 0.25;
    public double EaseLinearity { get; set; } = 0.25;
    public bool NoMoveStart { get; set; } = false;
    public int? MaxZoom { get; set; } = null;
    public Point PaddingTopLeft { get; set; } = new();
    public Point PaddingBottomRight { get; set; } = new();
    public Point Padding { get; set; } = new();
}