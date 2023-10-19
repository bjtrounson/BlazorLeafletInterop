using BlazorLeafletInterop.Models;
using BlazorLeafletInterop.Models.Options.Base;

namespace BlazorLeafletInterop.Components.Base;

public class InteractiveLayer : Layer
{
    public InteractiveLayerOptions InteractiveLayerOptions { get; set; } = new();
}