namespace BlazorLeafletInterop.Models.Options.Layer.Raster;

public class TileLayerOptions : BaseOptions
{
    public double MinZoom { get; set; } = 0;
    public double? MaxZoom { get; set; } = 18;
    public string[] SubDomains { get; set; } = { "a", "b", "c" };
    public string ErrorTileUrl { get; set; } = "";
    public int ZoomOffset { get; set; } = 0;
    public bool Tms { get; set; } = false;
    public bool ZoomReverse { get; set; } = false;
    public bool DetectRetina { get; set; } = false;
    public string? CrossOrigin { get; set; }
    public string? ReferrerPolicy { get; set; }

    
}