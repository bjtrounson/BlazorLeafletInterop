import type * as L from "leaflet";

export function createMiniMap(layer: L.Layer, options: any): any {
    // @ts-ignore
    return new L.Control.MiniMap(layer, options);
}

export function changeLayer(miniMap: any, layer: L.Layer): any {
    return miniMap.changeLayer(layer);
}