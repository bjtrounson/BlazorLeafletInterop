using BlazorLeafletInterop.Components.Base;
using BlazorLeafletInterop.Models.Basics;

namespace BlazorLeafletInterop.Models;

public class DivOverlayOptions : InteractiveLayer
{
    public bool Interactive { get; set; } = false;
    public Point Offset { get; set; } = new();
    public string ClassName { get; set; } = "";
    public string? Pane { get; set; }
    public string Content { get; set; } = "";
}