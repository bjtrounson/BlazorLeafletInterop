using System.ComponentModel;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;
using BlazorLeafletInterop.Models.Basics;

namespace BlazorLeafletInterop.Models;

[SupportedOSPlatform("browser")]
public class MapOptions
{
    #region Options

    public bool PreferCanvas { get; set; }

    #endregion

    #region Control options

    public bool AttributionControl { get; set; }
    public bool ZoomControl { get; set; }

    #endregion

    #region Interaction Options

    public bool ClosePopupOnClick { get; set; } = true;
    public bool BoxZoom { get; set; } = true;
    public string DoubleClickZoom { get; set; } = "true";
    public bool Dragging { get; set; } = true;
    public int ZoomSnap { get; set; } = 1;
    public int ZoomDelta { get; set; } = 1;
    public bool TrackResize { get; set; } = true;
    
    #endregion
    
    #region Panning Inertia Options
    
    public bool Inertia { get; set; }
    public int InertiaDeceleration { get; set; } = 3000;
    public int InertiaMaxSpeed { get; set; } = int.MaxValue;
    public double EaseLinearity { get; set; } = 0.2;
    public bool WorldCopyJump { get; set; }
    public double MaxBoundsViscosity { get; set; } = 0.0;
    
    #endregion
    
    #region Keyboard Navigation Options

    public bool Keyboard { get; set; } = true;
    public int KeyboardPanDelta { get; set; } = 80;
    
    #endregion

    #region Mouse wheel options

    public string ScrollWheelZoom { get; set; } = "true";
    public int WheelDebounceTime { get; set; } = 40;
    public int WheelPxPerZoomLevel { get; set; } = 60;

    #endregion

    #region Touch Interaction Options

    public bool Tap { get; set; }
    public int TapTolerance { get; set; } = 15;
    public string TouchZoom { get; set; } = string.Empty;
    public bool BounceAtZoomLimits { get; set; } = true;

    #endregion
    
    #region Map State Options
    
    public LatLng? Center { get; set; }
    public int? Zoom { get; set; }
    public int? MinZoom { get; set; }
    public int? MaxZoom { get; set; }
    public JSObject[] Layers { get; set; } = Array.Empty<JSObject>();
    public JSObject? MaxBounds { get; set; } = null;
    public JSObject? Renderer { get; set; } = null;
    
    #endregion
}