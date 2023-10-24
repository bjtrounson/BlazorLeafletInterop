import type * as L from "leaflet";

export function getContainer(layer: L.GridLayer): HTMLElement | null {
    return layer.getContainer();
}

export function setZIndex(layer: L.GridLayer, zIndex: number): L.GridLayer {
    return layer.setZIndex(zIndex);
}

export function isLoading(layer: L.GridLayer): boolean {
    return layer.isLoading();
}

export function redraw(layer: L.GridLayer): L.GridLayer {
    return layer.redraw();
}

export function getTileSize(layer: L.GridLayer): L.Point {
    return layer.getTileSize();
}