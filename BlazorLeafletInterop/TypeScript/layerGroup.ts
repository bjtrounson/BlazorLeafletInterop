import * as L from 'leaflet';

export function createLayerGroup(): L.LayerGroup {
    return L.layerGroup();
}

export function createFeatureGroup(): L.FeatureGroup {
    return L.featureGroup();
}

export function setStyle(layer: L.FeatureGroup, style: L.PathOptions): L.Layer {
    return layer.setStyle(style);
}

