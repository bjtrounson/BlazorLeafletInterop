using BlazorLeafletInterop.Models.Basics;

namespace BlazorLeafletInterop.Models.Options.Layer.UI;

public class PopupOptions : DivOverlayOptions
{
    public override string Pane { get; set; } = "popupPane";
    public override Point Offset { get; set; } = new(0, 7);
    public int MaxWidth { get; set; } = 300;
    public int MinWidth { get; set; } = 50;
    public int? MaxHeight { get; set; }
    public bool AutoPan { get; set; } = true;
    public Point? AutoPanPaddingTopLeft { get; set; }
    public Point? AutoPanPaddingBottomRight { get; set; }
    public Point AutoPanPadding { get; set; } = new(5, 5);
    public bool KeepInView { get; set; } = false;
    public bool CloseButton { get; set; } = true;
    public bool AutoClose { get; set; } = true;
    public bool? CloseOnClick { get; set; }
    public override string ClassName { get; set; } = "";
}