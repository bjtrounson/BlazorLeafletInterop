using System.Runtime.Versioning;
using BlazorLeafletInterop.Models.Basics;
using Microsoft.JSInterop;

namespace BlazorLeafletInterop.Models;

public class MarkerOptions : InteractiveLayerOptions
{
    public bool Keyboard { get; set; } = true;
    public string Title { get; set; } = "";
    public string Alt { get; set; } = "";
    public double ZIndexOffset { get; set; } = 0;
    public double Opacity { get; set; } = 1.0;
    public bool RiseOnHover { get; set; } = false;
    public int RiseOffset { get; set; } = 250;
    public override string Pane { get; set; } = "markerPane";
    public string ShadowPane { get; set; } = "shadowPane";
    public override bool BubblingMouseEvents { get; set; } = false;
    public bool AutoPanOnFocus { get; set; } = true;

    #region Draggable Marker Options

    public bool Draggable { get; set; } = false;
    public bool AutoPan { get; set; } = false;
    public Point AutoPanPadding { get; set; } = new(50, 50);
    public double AutoPanSpeed { get; set; } = 10.0;

    #endregion
}