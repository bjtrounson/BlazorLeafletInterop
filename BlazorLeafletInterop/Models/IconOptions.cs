using BlazorLeafletInterop.Models.Basics;

namespace BlazorLeafletInterop.Models;

public class IconOptions
{
    public string IconUrl { get; set; } = default!;
    public string? IconRetinaUrl { get; set; }
    public Point? IconSize { get; set; }
    public Point? IconAnchor { get; set; }
    public Point? PopupAnchor { get; set; } = new();
    public Point? TooltipAnchor { get; set; } = new();
    public string? ShadowUrl { get; set; }
    public string? ShadowRetinaUrl { get; set; }
    public Point? ShadowSize { get; set; }
    public Point? ShadowAnchor { get; set; }
    public string ClassName { get; set; } = "";
    public string CrossOrigin { get; set; } = "false";
}