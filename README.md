# Blazor Leaflet Interop
**WARNING: This is only works with Blazor WASM and not Blazor Server, since it uses the new JSImport / JSExport API** \
It is a work in progress and only supports a small subset of the Leaflet API. 

## Before you start
Add this to your Program.cs
```csharp
await MapBundleImport.ImportAsync();
```

## Simple Usage
To make a simple map use the following code:
```csharp
@page "/"

@using BlazorLeafletInterop.Components
@using BlazorLeafletInterop.Components.Base
@using BlazorLeafletInterop.Models
@using BlazorLeafletInterop.Models.Basics

<Map @ref="Map" Class="map" MapOptions="Options">
    <TileLayer @ref="TileLayer" UrlTemplate="https://tile.openstreetmap.org/{z}/{x}/{y}.png" />
    <Marker Icon="IconRef" LatLng="new LatLng(50, 9)">
        <Popup>
            Testing Popup Content
        </Popup>
        <Tooltip>
            Testing Tooltip Content
        </Tooltip>
    </Marker>
</Map>

@code {
    private Map? Map { get; set; }
    private TileLayer? TileLayer { get; set; }
    private MapOptions Options { get; set; } = new() { Center = new LatLng(50, 9), Zoom = 13 };
    private Icon? IconRef { get; set; }

    protected override async Task OnInitializedAsync()
    {
	    var options = new IconOptions()
	    {
		    IconUrl = "https://leafletjs.com/examples/custom-icons/leaf-green.png", 
		    IconSize = new Point(38, 95), 
		    IconAnchor = new Point(22, 94), 
		    PopupAnchor = new Point(-3, -76), 
		    ShadowUrl = "https://leafletjs.com/examples/custom-icons/leaf-shadow.png", 
		    ShadowSize = new Point(50, 64), 
		    ShadowAnchor = new Point(4, 62), 
		    ClassName = "my-div-icon"
	    };
	    IconRef = new Icon(options);
	}
}
```

