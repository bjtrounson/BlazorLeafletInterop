import type * as L from "leaflet";
import DotNetObject = DotNet.DotNetObject;

export function hasLayer(layerGroup: L.LayerGroup | L.Map, layer: L.Layer): boolean {
    return layerGroup.hasLayer(layer);
}

export function clearLayers(layerGroup: L.LayerGroup): L.LayerGroup {
    return layerGroup.clearLayers();
}

export function invoke(layerGroup: L.LayerGroup, method: string, ...args: any[]): L.LayerGroup {
    return layerGroup.invoke(method, ...args);
}

export function getLayer(layerGroup: L.LayerGroup, id: number): L.Layer {
    return layerGroup.getLayer(id);
}

export function getLayers(layerGroup: L.LayerGroup): L.Layer[] {
    return layerGroup.getLayers();
}

export function getLayerId(layerGroup: L.LayerGroup, layer: L.Layer): number {
    return layerGroup.getLayerId(layer);
}

export function setStyle(layer: L.FeatureGroup, style: L.PathOptions): L.Layer {
    return layer.setStyle(style);
}
