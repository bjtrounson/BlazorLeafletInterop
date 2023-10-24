import type * as L from "leaflet";

export function createLayerGroup(): L.LayerGroup {
    // @ts-ignore
    return L.layerGroup();
}

export function createFeatureGroup(): L.FeatureGroup {
    // @ts-ignore
    return L.featureGroup();
}

export function setStyle(layer: L.FeatureGroup, style: L.PathOptions): L.Layer {
    return layer.setStyle(style);
}

