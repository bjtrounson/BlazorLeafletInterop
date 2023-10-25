using BlazorLeafletInterop.Models.Options.Base;

namespace BlazorLeafletInterop.Models.Options.Control;

public class ControlLayersOptions : ControlOptions
{
    public bool Collapsed { get; set; } = true;
    public bool AutoZIndex { get; set; } = true;
    public bool HideSingleBase { get; set; } = false;
    public bool SortLayers { get; set; } = false;
}