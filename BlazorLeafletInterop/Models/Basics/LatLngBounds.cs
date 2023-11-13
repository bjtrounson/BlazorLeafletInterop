using System.Text.Json.Serialization;

namespace BlazorLeafletInterop.Models.Basics;

public class LatLngBounds
{
    public LatLngBounds()
    {
        SouthWest = new LatLng();
        NorthEast = new LatLng();
    }

    public LatLngBounds(LatLng southwest, LatLng northeast)
    {
        SouthWest = southwest;
        NorthEast = northeast;
    }

    // ReSharper disable once MemberCanBePrivate.Global
    [JsonPropertyName("_southWest")]
    public LatLng SouthWest { get; set; }
    // ReSharper disable once MemberCanBePrivate.Global
    [JsonPropertyName("_northEast")]
    public LatLng NorthEast { get; set; }

    public LatLngBounds Extend(LatLng point)
    {
        SouthWest = new LatLng(Math.Min(point.Lat, SouthWest.Lat), Math.Min(point.Lng, SouthWest.Lng));
        NorthEast = new LatLng(Math.Max(point.Lat, NorthEast.Lat), Math.Max(point.Lng, NorthEast.Lng));

        return this;
    }
    
    public LatLngBounds Extend(LatLngBounds bounds)
    {
        SouthWest = new LatLng(Math.Min(bounds.SouthWest.Lat, SouthWest.Lat), Math.Min(bounds.SouthWest.Lng, SouthWest.Lng));
        NorthEast = new LatLng(Math.Max(bounds.NorthEast.Lat, NorthEast.Lat), Math.Max(bounds.NorthEast.Lng, NorthEast.Lng));

        return this;
    }
    
    public LatLngBounds Pad(double bufferRatio)
    {
        var heightBuffer = (NorthEast.Lat - SouthWest.Lat) * bufferRatio;
        var widthBuffer = (NorthEast.Lng - SouthWest.Lng) * bufferRatio;

        return new LatLngBounds(
            new LatLng(SouthWest.Lat - heightBuffer, SouthWest.Lng - widthBuffer),
            new LatLng(NorthEast.Lat + heightBuffer, NorthEast.Lng + widthBuffer));
    }
    
    public LatLng GetCenter() => new ((SouthWest.Lat + NorthEast.Lat) / 2, (SouthWest.Lng + NorthEast.Lng) / 2);
    public LatLng GetNorthWest() => new (NorthEast.Lat, SouthWest.Lng);
    public LatLng GetSouthEast() => new (SouthWest.Lat, NorthEast.Lng);
    public LatLng GetWest() => new ((SouthWest.Lat + NorthEast.Lat) / 2, SouthWest.Lng);
    public LatLng GetEast() => new ((SouthWest.Lat + NorthEast.Lat) / 2, NorthEast.Lng);
    public LatLng GetNorth() => new (NorthEast.Lat, (SouthWest.Lng + NorthEast.Lng) / 2);
    public LatLng GetSouth() => new (SouthWest.Lat, (SouthWest.Lng + NorthEast.Lng) / 2);
    
    public bool Contains(LatLngBounds bounds)
    {
        return SouthWest.Lat <= bounds.SouthWest.Lat &&
               SouthWest.Lng <= bounds.SouthWest.Lng &&
               NorthEast.Lat >= bounds.NorthEast.Lat &&
               NorthEast.Lng >= bounds.NorthEast.Lng;
    }

    public bool Contains(LatLng point)
    {
        return SouthWest.Lat <= point.Lat &&
               SouthWest.Lng <= point.Lng &&
               NorthEast.Lat >= point.Lat &&
               NorthEast.Lng >= point.Lng;
    }
    
    public bool Intersects(LatLngBounds bounds)
    {
        var sw = SouthWest;
        var ne = NorthEast;
        var sw2 = bounds.SouthWest;
        var ne2 = bounds.NorthEast;
        var latIntersects = (ne2.Lat >= sw.Lat) && (sw2.Lat <= ne.Lat);
        var lngIntersects = (ne2.Lng >= sw.Lng) && (sw2.Lng <= ne.Lng);

        return latIntersects && lngIntersects;
    }
    
    public bool Overlaps(LatLngBounds bounds)
    {
        var sw = SouthWest;
        var ne = NorthEast;
        var sw2 = bounds.SouthWest;
        var ne2 = bounds.NorthEast;
        var latOverlaps = (ne2.Lat > sw.Lat) && (sw2.Lat < ne.Lat);
        var lngOverlaps = (ne2.Lng > sw.Lng) && (sw2.Lng < ne.Lng);

        return latOverlaps && lngOverlaps;
    }
    
    public string ToBBoxString()
    {
        return $"{SouthWest.Lng},{SouthWest.Lat},{NorthEast.Lng},{NorthEast.Lat}";
    }
    
    public bool Equals(LatLngBounds bounds)
    {
        return SouthWest.Equals(bounds.SouthWest) && NorthEast.Equals(bounds.NorthEast);
    }
}