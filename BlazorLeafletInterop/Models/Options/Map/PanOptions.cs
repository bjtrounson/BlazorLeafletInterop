namespace BlazorLeafletInterop.Models.Options.Map;

public class PanOptions : ZoomOptions
{
    public double Duration { get; set; } = 0.25;
    public double EaseLinearity { get; set; } = 0.25;
    public bool NoMoveStart { get; set; } = false;
}