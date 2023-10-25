using BlazorLeafletInterop.Models.Options.Base;
using BlazorLeafletInterop.Models.Options.Layer.Vector;
using BlazorLeafletInterop.Models.Options.Map;

namespace BlazorLeafletInterop.Plugins.Leaflet.MiniMap.Models.Options;

public class LeafletMiniMapOptions : ControlOptions
{
    public override string Position { get; set; } = "bottomright";
    public int Width { get; set; } = 150;
    public int Height { get; set; } = 150;
    public int CollapsedWidth { get; set; } = 19;
    public int CollapsedHeight { get; set; } = 19;
    public int ZoomLevelOffset { get; set; } = -5;
    public bool ZoomLevelFixed { get; set; } = false;
    public bool CenterFixed { get; set; } = false;
    public bool ZoomAnimation { get; set; } = false;
    public bool ToggleDisplay { get; set; } = false;
    public bool AutoToggleDisplay { get; set; } = false;
    public bool Minimized { get; set; } = false;
    public PathOptions AimingRectOptions { get; set; } =
        new() { Color = "#ff7800", Weight = 1, Interactive = false};
    public PathOptions ShadowRectOptions { get; set; } =
        new() { Color = "#000000", Weight = 1, Opacity = 0, FillOpacity = 0, Interactive = false};
    public Dictionary<string, string> Strings { get; set; } = new()
    {
        {"hideText", "Hide MiniMap"},
        {"showText", "Show MiniMap"}
    };
    public MapOptions? MapOptions { get; set; } = null;
    
}