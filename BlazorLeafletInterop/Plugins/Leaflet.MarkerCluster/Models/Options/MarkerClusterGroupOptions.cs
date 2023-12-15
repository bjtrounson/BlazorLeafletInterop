namespace BlazorLeafletInterop.Plugins.Leaflet.MarkerCluster.Models.Options;

public class MarkerClusterGroupOptions
{
    public int MaxClusterRadius { get; set; } = 80;
    public bool SpiderfyOnEveryZoom { get; set; } = true;
    public bool ShowCoverageOnHover { get; set; } = true;
    public bool ZoomToBoundsOnClick { get; set; } = true;
    public bool SingleMarkerMode { get; set; } = false;
    public int? DisableClusteringAtZoom { get; set; } = null;
    public bool RemoveOutsideVisibleBounds { get; set; } = true;
    public bool Animate { get; set; } = true;
    public bool AnimateAddingMarkers { get; set; } = false;
    public int SpiderfyDistanceMultiplier { get; set; } = 1;
    public bool ChunkedLoading { get; set; } = true;
    public int ChunkInterval { get; set; } = 200;
    public int ChunkDelay { get; set; } = 50;
}