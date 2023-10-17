using System.Runtime.Versioning;
using BlazorLeafletInterop.Models.Basics;

namespace BlazorLeafletInterop.Models.Map;

[SupportedOSPlatform("browser")]
public class PaddingOptions : BaseOptions
{
    public Point PaddingTopLeft { get; set; } = new();
    public Point PaddingBottomRight { get; set; } = new();
    public Point Padding { get; set; } = new();
}