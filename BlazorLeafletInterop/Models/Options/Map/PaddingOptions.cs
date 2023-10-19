using BlazorLeafletInterop.Models.Basics;

namespace BlazorLeafletInterop.Models.Options.Map;

public class PaddingOptions : BaseOptions
{
    public Point PaddingTopLeft { get; set; } = new();
    public Point PaddingBottomRight { get; set; } = new();
    public Point Padding { get; set; } = new();
}