using BlazorLeafletInterop.Models.Basics;
using BlazorLeafletInterop.Models.Options.Base;

namespace BlazorLeafletInterop.Models.Options.Layer.UI;

public class DivOverlayOptions : InteractiveLayerOptions
{
    public override bool Interactive { get; set; } = false;
    public virtual Point Offset { get; set; } = new();
    public virtual string ClassName { get; set; } = "";
    public override string? Pane { get; set; }
    public string Content { get; set; } = "";
}