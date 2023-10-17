using System.Runtime.Versioning;

namespace BlazorLeafletInterop.Models.Map;

[SupportedOSPlatform("browser")]
public class ZoomOptions : BaseOptions
{
    public bool? Animate { get; set; } = null;
}