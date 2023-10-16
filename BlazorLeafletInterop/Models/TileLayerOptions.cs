namespace BlazorLeafletInterop.Models;

public class TileLayerOptions : GridLayerOptions
{
    public override double MinZoom { get; set; } = 0;
    public override double? MaxZoom { get; set; } = 18;
    public string[] SubDomains { get; set; } = { "a", "b", "c" };
    public string ErrorTileUrl { get; set; } = "";
    public int ZoomOffset { get; set; } = 0;
    public bool Tms { get; set; } = false;
    public bool ZoomReverse { get; set; } = false;
    public bool DetectRetina { get; set; } = false;
    public string CrossOrigin { get; set; } = "false";
    public string ReferrerPolicy { get; set; } = "false";

    
}