﻿import type * as L from "leaflet";
import DotNetObject = DotNet.DotNetObject;

export * from "./evented";
export * from "./layer";
export * from "./gridLayer";
export * from "./marker";
export * from "./icon";
export * from "./divOverlay";
export * from "./layerGroup";
export * from "./geoJson";
export * from "./map";
export * from "./vectorLayer";
export * from "./control"
export * from "./Plugins/leaflet-minimap";
export * from "./Plugins/leaflet-markercluster";

export function getLatLng(divOverlay: L.DivOverlay | L.Marker): string {
    return JSON.stringify(divOverlay.getLatLng());
}

export function setLatLng(divOverlay: L.DivOverlay | L.Marker, latLng: L.LatLngExpression): L.DivOverlay | L.Marker {
    return divOverlay.setLatLng(latLng);
}

export function addLayer(layerGroup: L.LayerGroup | L.Map, layer: L.Layer): L.LayerGroup | L.Map {
    return layerGroup.addLayer(layer);
}

export function removeLayer(layerGroup: L.LayerGroup | L.Map | L.Control.Layers, layer: L.Layer): L.LayerGroup | L.Map | L.Control.Layers {
    return layerGroup.removeLayer(layer);
}

export function eachLayer(instance: DotNetObject, callbackMethod: string, map: L.Map | L.LayerGroup): L.Map | L.LayerGroup {
    return map.eachLayer((layer: L.Layer) => instance.invokeMethod(callbackMethod, DotNet.createJSObjectReference(layer)));
}

export function getBounds(layer: L.FeatureGroup | L.Map): string {
    return JSON.stringify(layer.getBounds());
}

export function setOpacity(layer: L.GridLayer | L.Marker, opacity: number): L.GridLayer | L.Marker {
    return layer.setOpacity(opacity);
}

export function bringToFront(layer: L.GridLayer | L.FeatureGroup): L.GridLayer | L.FeatureGroup | L.DivOverlay {
    return layer.bringToFront();
}

export function bringToBack(layer: L.GridLayer | L.FeatureGroup): L.GridLayer | L.FeatureGroup | L.DivOverlay {
    return layer.bringToBack();
}

export function getElementInnerHtml(id: string): string {
    const element = document.getElementById(id);
    if (element == null) return null;
    if (element.innerHTML == null) return null;
    return element.innerHTML;
}

export function setUrl(layer: L.TileLayer, url: string, noRedraw: boolean): L.TileLayer {
    return layer.setUrl(url, noRedraw);
}

export function jsonToJsObject(json: string) {
    return JSON.parse(json);
}
export function createMap(id: string, options: L.MapOptions): L.Map {
    // @ts-ignore
    return L.map(id, options);
}

export function createTileLayer(urlTemplate: string, options: L.TileLayerOptions): L.TileLayer {
    // @ts-ignore
    return L.tileLayer(urlTemplate, options);
}

export function createLayerGroup(): L.LayerGroup {
    // @ts-ignore
    return L.layerGroup();
}

export function createFeatureGroup(): L.FeatureGroup {
    // @ts-ignore
    return L.featureGroup();
}

export function getContainer(layer: L.GridLayer | L.Control): HTMLElement | null {
    return layer.getContainer();
}