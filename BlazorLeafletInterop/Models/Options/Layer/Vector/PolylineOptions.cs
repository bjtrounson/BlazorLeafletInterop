namespace BlazorLeafletInterop.Models.Options.Layer.Vector;

public class PolylineOptions : PathOptions
{
    public double SmoothFactor { get; set; } = 1.0;
    public bool NoClip { get; set; } = false;
}