import * as L from "leaflet";

export function jsonToObject(json: string) {
    return JSON.parse(json);
}
export function createMap(id: string, options: L.MapOptions): L.Map {
    console.log(options)
    return L.map(id, options);
}

export function createTileLayer(urlTemplate: string, options: L.TileLayerOptions): L.TileLayer {
    return L.tileLayer(urlTemplate, options);
}

export function addTileLayer(tileLayer: L.TileLayer, layer: L.Map | L.LayerGroup): L.TileLayer {
    return tileLayer.addTo(layer);
}