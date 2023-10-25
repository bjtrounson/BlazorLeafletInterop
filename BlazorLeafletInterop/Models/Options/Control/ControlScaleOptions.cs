using BlazorLeafletInterop.Models.Options.Base;

namespace BlazorLeafletInterop.Models.Options.Control;

public class ControlScaleOptions : ControlOptions
{
    public int MaxWidth { get; set; } = 100;
    public bool Metric { get; set; } = true;
    public bool Imperial { get; set; } = true;
    public bool UpdateWhenIdle { get; set; } = false;
}