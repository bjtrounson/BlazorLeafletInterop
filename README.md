# Blazor Leaflet Interop
**It is a work in progress and only supports parts of the Leaflet API.** \
This project is a component based wrapper around the Leaflet API, but some components can be used outside of RenderTree.

## Implemented Components
- [x] Map
- [x] TileLayer
- [x] Marker
- [x] Popup, Tooltip
- [x] Polyline, MultiPolyline
- [x] FeatureGroup, LayerGroup, GeoJSON
- [x] Control.Zoom, Control.Scale, Control.Attribution, Control.Layers
- [x] Icon
- [x] Events (**should work but are not tested**)
- [ ] ImageOverlay, SVGOverlay, VideoOverlay
- [ ] Renderers
- [ ] Rectangle, Polygon, Circle, CircleMarker
- [ ] Bounds, DivIcon

## Installation
Install the package from NuGet
```bash
dotnet add package BlazorLeaflet
```
```powershell
Install-Package BlazorLeafletInterop
```
Add the latest leaflet version to your index.html
```html
<!-- ...index.html -->
<script src="https://cdn.jsdelivr.net/npm/leaflet@1.9.4/dist/leaflet.min.js"></script>
```
Add this service to your Program.cs
```csharp
// ...Program.cs
builder.Services.AddMapService();
```

## Examples
To make a simple map use the following code
```csharp
<Map MapOptions="Options">
    <TileLayer UrlTemplate="https://tile.openstreetmap.org/{z}/{x}/{y}.png" />
</Map>

@code {
	private MapOptions Options = new MapOptions() {
		Center = new LatLng(51.505, -0.09),
		Zoom = 13
	};
}
```

### Marker Usage
```csharp
<Map MapOptions="Options">
    <TileLayer UrlTemplate="https://tile.openstreetmap.org/{z}/{x}/{y}.png" />
    <Marker LatLng="new LatLng(51.5, -0.0.9)"></Marker>
</Map>

@code {
	private MapOptions Options = new MapOptions() {
		Center = new LatLng(51.505, -0.09),
		Zoom = 13
	};
}
```

### Popup Usage
```csharp
<Map MapOptions="Options">
    <TileLayer UrlTemplate="https://tile.openstreetmap.org/{z}/{x}/{y}.png" />
    <Marker LatLng="new LatLng(51.5, -0.0.9)">
	    <Popup>
		    <b>Hello world!</b><br>
		    I am a popup
	    </Popup>
    </Marker>
</Map>

@code {
	private MapOptions Options = new MapOptions() {
		Center = new LatLng(51.505, -0.09),
		Zoom = 13
	};
}
```
### Accessing Leaflet Method's
```csharp
<Map MapOptions="Options">
    <TileLayer UrlTemplate="https://tile.openstreetmap.org/{z}/{x}/{y}.png" />
    <Marker @ref="MarkerRef" LatLng="new LatLng(51.5, -0.0.9)">
	    <Popup>
		    <b>Hello world!</b><br>
		    I am a popup
	    </Popup>
    </Marker>
</Map>

@code {
	// Creating a reference to the component,
	// gives you access to the Leaflet methods for that class.
	private Marker? MarkerRef;
	private MapOptions Options = new MapOptions() {
		Center = new LatLng(51.505, -0.09),
		Zoom = 13
	};

	protected override async Task OnAfterRenderAsync(bool firstRender) 
	{
		if (MarkerRef is not null) await MarkerRef.OpenPopup();
	}
}
```

## Contributing
If you want to contribute to this project, feel free to do so. Just fork the project and create a pull request.

