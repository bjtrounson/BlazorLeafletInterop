using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;
using BlazorLeafletInterop.Interops;

namespace BlazorLeafletInterop.Models.Basics;

[SupportedOSPlatform("browser")]
public class LatLng
{
    public LatLng()
    {
    }

    public LatLng(double lat, double lng)
    {
        Lat = lat;
        Lng = lng;
        Alt = 0;
    }

    public LatLng(double lat, double lng, double alt)
    {
        Lat = lat;
        Lng = lng;
        Alt = alt;
    }

    public double Lat { get; set; }
    public double Lng { get; set; }
    public double Alt { get; set; }
    
    public double GetDistanceToPointInMiles(LatLng to)
    {
        var radLat1 = Lat * Math.PI / 180;
        var radLat2 = to.Lat * Math.PI / 180;
        var dLatHalf = (radLat2 - radLat1) / 2;
        var dLonHalf = Math.PI * (to.Lng - Lng) / 360;

        // intermediate result
        var a = Math.Sin(dLatHalf);
        a *= a;

        // intermediate result
        var b = Math.Sin(dLonHalf);
        b *= b * Math.Cos(radLat1) * Math.Cos(radLat2);

        // central angle, aka arc segment angular distance
        var centralAngle = 2 * Math.Atan2(Math.Sqrt(a + b), Math.Sqrt(1 - a - b));

        // great-circle (orthodromic) distance on Earth between 2 points
        return 3959 * centralAngle;
    }
    
    public double GetDistanceToPointInKilometers(LatLng to)
    {
        return GetDistanceToPointInMiles(to) * 1.60934;
    }
    
    public double GetDistanceToPointInMeters(LatLng to)
    {
        return GetDistanceToPointInKilometers(to) * 1000;
    }
    
    public object ToJsObject()
    {
        return LeafletInterop.JsonToJsObject(LeafletInterop.ObjectToJson(this));
    }
}