import type * as L from "leaflet";
type LatLngBounds = {
    _southWest: L.LatLng;
    _northEast: L.LatLng;
}
export function addControl(map: L.Map, control: L.Control): L.Map {
    return map.addControl(control);
}

export function removeControl(map: L.Map, control: L.Control): L.Map {
    return map.removeControl(control);
}

export function removeLayer(map: L.Map, layer: L.Layer): L.Map {
    return map.removeLayer(layer);
}

export function HasLayer(map: L.Map, layer: L.Layer): boolean {
    return map.hasLayer(layer);
}

export function openMapPopup(map: L.Map, popup: L.Popup): L.Map {
    return map.openPopup(popup);
}

export function closeMapPopup(map: L.Map, popup: L.Popup): L.Map {
    return map.closePopup(popup);
}

export function openMapTooltip(map: L.Map, tooltip: L.Tooltip): L.Map {
    return map.openTooltip(tooltip);
}

export function closeMapTooltip(map: L.Map, tooltip: L.Tooltip): L.Map {
    return map.closeTooltip(tooltip);
}

export function setView(map: L.Map, center: L.LatLngExpression, zoom: number, options?: L.ZoomPanOptions): L.Map {
    return map.setView(center, zoom, options);
}

export function setZoom(map: L.Map, zoom: number, options?: L.ZoomPanOptions): L.Map {
    return map.setZoom(zoom, options);
}

export function zoomIn(map: L.Map, delta?: number, options?: L.ZoomOptions): L.Map {
    return map.zoomIn(delta, options);
}

export function zoomOut(map: L.Map, delta?: number, options?: L.ZoomOptions): L.Map {
    return map.zoomOut(delta, options);
}

export function setZoomAround(map: L.Map, latlng: L.LatLngExpression, zoom: number, options?: L.ZoomOptions): L.Map {
    return map.setZoomAround(latlng, zoom, options);
}

export function fitBounds(map: L.Map, bounds: LatLngBounds, options?: L.FitBoundsOptions): L.Map {
    // @ts-ignore
    const newBounds = L.latLngBounds(bounds._southWest, bounds._northEast);
    return map.fitBounds(newBounds, options);
}

export function fitWorld(map: L.Map, options?: L.FitBoundsOptions): L.Map {
    return map.fitWorld(options);
}

export function panTo(map: L.Map, latlng: L.LatLngExpression, options?: L.PanOptions): L.Map {
    return map.panTo(latlng, options);
}

export function panBy(map: L.Map, offset: L.PointExpression): L.Map {
    return map.panBy(offset);
}

export function flyTo(map: L.Map, latlng: L.LatLngExpression, zoom?: number, options?: L.ZoomPanOptions): L.Map {
    return map.flyTo(latlng, zoom, options);
}

export function flyToBounds(map: L.Map, bounds: LatLngBounds, options?: L.FitBoundsOptions): L.Map {
    // @ts-ignore
    const newBounds = L.latLngBounds(bounds._southWest, bounds._northEast);
    return map.flyToBounds(newBounds, options);
}

export function setMaxBounds(map: L.Map, bounds: LatLngBounds): L.Map {
    // @ts-ignore
    const newBounds = L.latLngBounds(bounds._southWest, bounds._northEast);
    return map.setMaxBounds(newBounds);
}

export function setMinZoom(map: L.Map, zoom: number): L.Map {
    return map.setMinZoom(zoom);
}

export function setMaxZoom(map: L.Map, zoom: number): L.Map {
    return map.setMaxZoom(zoom);
}

export function panInsideBounds(map: L.Map, bounds: LatLngBounds, options?: L.PanOptions): L.Map {
    // @ts-ignore
    const newBounds = L.latLngBounds(bounds._southWest, bounds._northEast);
    return map.panInsideBounds(newBounds, options);
}

export function panInside(map: L.Map, latlng: L.LatLngExpression, options?: L.PanOptions): L.Map {
    return map.panInside(latlng, options);
}

export function invalidateSize(map: L.Map, animate: boolean): L.Map {
    return map.invalidateSize(animate);
}

export function stop(map: L.Map): L.Map {
    return map.stop();
}

export function getCenter(map: L.Map): string {
    return JSON.stringify(map.getCenter());
}

export function getZoom(map: L.Map): number {
    return map.getZoom();
}

export function getBounds(map: L.Map): string {
    return JSON.stringify(map.getBounds());
}

export function getMinZoom(map: L.Map): number {
    return map.getMinZoom();
}

export function getMaxZoom(map: L.Map): number {
    return map.getMaxZoom();
}

export function getBoundsZoom(map: L.Map, bounds: LatLngBounds, inside?: boolean, padding?: L.Point): number {
    // @ts-ignore
    const newBounds = L.latLngBounds(bounds._southWest, bounds._northEast);
    return map.getBoundsZoom(newBounds, inside, padding);
}

export function getSize(map: L.Map): string {
    return JSON.stringify(map.getSize());
}

export function getPixelBounds(map: L.Map): string {
    return JSON.stringify(map.getPixelBounds());
}

export function getPixelOrigin(map: L.Map): string {
    return JSON.stringify(map.getPixelOrigin());
}

export function getPixelWorldBounds(map: L.Map): string {
    return JSON.stringify(map.getPixelWorldBounds());
}
