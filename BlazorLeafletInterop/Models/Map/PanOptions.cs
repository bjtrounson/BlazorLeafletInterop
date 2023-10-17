using System.Runtime.Versioning;

namespace BlazorLeafletInterop.Models.Map;

[SupportedOSPlatform("browser")]
public class PanOptions : ZoomOptions
{
    public double Duration { get; set; } = 0.25;
    public double EaseLinearity { get; set; } = 0.25;
    public bool NoMoveStart { get; set; } = false;
}