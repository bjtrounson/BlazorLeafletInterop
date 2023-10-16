namespace BlazorLeafletInterop.Models;

public class LayerOptions
{
    public virtual string Pane { get; set; } = "overlayPane";
    public string? Attribution { get; set; }
}