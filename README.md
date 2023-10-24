# Blazor Leaflet Interop
**It is a work in progress and only supports parts of the Leaflet API.** \
**I have only tested this on Blazor WASM but should work with Blazor Server.**

## Implemented Features
- Map
- Events
- Base Layers
    - GridLayer
    - Layer
    - InteractiveLayer
- Raster Layers
  - TileLayer
- UI Layers
  - Marker
  - Popup
  - Tooltip
  - DivOverlay
- Basic
  - LatLng
  - LatLngBounds
  - Icon (Create this via the IconFactoryInterop service)
  - Point
- Other Layers
  - GeoJSON
  - FeatureGroup
  - LayerGroup
- Vector Layers
  - Path
  - Polyline

**Events should work but are not tested yet.**
## Before you start
Add the latest leaflet version to your index.html
```html
<script src="https://cdn.jsdelivr.net/npm/leaflet@1.9.4/dist/leaflet.min.js"></script>
```
Add this service to your Program.cs
```csharp
builder.Services.AddMapService();
```

## Simple Usage
To make a simple map use the following code:
```csharp
@page "/"

@using BlazorLeafletInterop.Components
@using BlazorLeafletInterop.Components.Base
@using BlazorLeafletInterop.Models
@using BlazorLeafletInterop.Models.Basics

<Map Class="map" MapOptions="Options">
    <TileLayer @ref="TileLayer" UrlTemplate="https://tile.openstreetmap.org/{z}/{x}/{y}.png" />
    <Marker LatLng="new LatLng(50, 9)">
        <Popup>
            Testing Popup Content
        </Popup>
        <Tooltip>
            Testing Tooltip Content
        </Tooltip>
    </Marker>
</Map>

@code {
    private MapOptions Options { get; set; } = new() { Center = new LatLng(50, 9), Zoom = 13 };
}
```

## TODO
- Add Controls
- Add ImageOverlay, SVGOverlay, VideoOverlay
- Add Renderers
- Add Rectangle, Polygon, Circle, CircleMarker
- Add Bounds, DivIcon
- Plugin Support

## Contributing
If you want to contribute to this project, feel free to do so. Just fork the project and create a pull request.

