namespace BlazorLeafletInterop.Models.Options.Base;

public class InteractiveLayerOptions : LayerOptions
{
    public virtual bool Interactive { get; set; } = true;
    public virtual bool BubblingMouseEvents { get; set; } = true;
}