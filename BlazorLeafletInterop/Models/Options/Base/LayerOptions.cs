namespace BlazorLeafletInterop.Models.Options.Base;

public class LayerOptions : BaseOptions
{
    public virtual string Pane { get; set; } = "overlayPane";
    public string? Attribution { get; set; }
}