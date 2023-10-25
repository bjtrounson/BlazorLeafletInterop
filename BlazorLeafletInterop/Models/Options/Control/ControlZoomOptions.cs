namespace BlazorLeafletInterop.Models.Options.Control;

public class ControlZoomOptions
{
    public string ZoomInText { get; set; } = "<span aria-hidden=\"true\">+</span>";
    public string ZoomOutText { get; set; } = "<span aria-hidden=\"true\">&#x2212;</span>";
    public string ZoomInTitle { get; set; } = "Zoom in";
    public string ZoomOutTitle { get; set; } = "Zoom out";
}