using BlazorLeafletInterop.Models;

namespace BlazorLeafletInterop.Components.Base;

public class InteractiveLayer : Layer
{
    public InteractiveLayerOptions InteractiveLayerOptions { get; set; } = new();
}