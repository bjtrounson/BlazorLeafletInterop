import * as L from 'leaflet';

export function createMarker(latLng: L.LatLngExpression, options?: L.MarkerOptions): L.Marker {
    return L.marker(latLng, options);
}

export function getLatLng(marker: L.Marker): L.LatLng {
    return marker.getLatLng();
}

export function setLatLng(marker: L.Marker, latLng: L.LatLng): void {
    marker.setLatLng(latLng);
}

export function getIcon(marker: L.Marker): L.Icon | L.DivIcon {
    return marker.getIcon();
}

export function setIcon(marker: L.Marker, icon: L.Icon): void {
    marker.setIcon(icon);
}

export function setZIndexOffset(marker: L.Marker, zIndexOffset: number): void {
    marker.setZIndexOffset(zIndexOffset);
}

export function toGeoJSON(marker: L.Marker, precision: number | undefined | null): any {
    return marker.toGeoJSON(precision);
}