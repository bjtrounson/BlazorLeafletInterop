using BlazorLeafletInterop.Models.Basics;

namespace BlazorLeafletInterop.Models.Options.Layer.UI;

public class TooltipOptions : DivOverlayOptions
{
    public override string Pane { get; set; } = "tooltipPane";
    public override Point Offset { get; set; } = new();
    public string Direction { get; set; } = "auto";
    public bool Permanent { get; set; } = false;
    public bool Sticky { get; set; } = false;
    public double Opacity { get; set; } = 0.9;
}