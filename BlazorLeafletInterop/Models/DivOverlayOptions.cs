using BlazorLeafletInterop.Components.Base;
using BlazorLeafletInterop.Models.Basics;

namespace BlazorLeafletInterop.Models;

public class DivOverlayOptions : InteractiveLayerOptions
{
    public override bool Interactive { get; set; } = false;
    public virtual Point Offset { get; set; } = new();
    public virtual string ClassName { get; set; } = "";
    public override string? Pane { get; set; }
    public string Content { get; set; } = "";
}