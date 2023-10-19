import * as L from 'leaflet';

export function createPolyline(latlngs: L.LatLngExpression[], options: L.PolylineOptions): L.Polyline {
    return L.polyline(latlngs, options);
}

export function createPolygon(latlngs: L.LatLngExpression[], options: L.PolylineOptions): L.Polygon {
    return L.polygon(latlngs, options);
}

export function createRectangle(bounds: L.LatLngBoundsExpression, options: L.PolylineOptions): L.Rectangle {
    return L.rectangle(bounds, options);
}

export function createCircle(latlng: L.LatLngExpression, radius: number, options: L.PathOptions): L.Circle {
    return L.circle(latlng, radius, options);
}

export function createCircleMarker(latlng: L.LatLngExpression, options: L.PathOptions): L.CircleMarker {
    return L.circleMarker(latlng, options);
}

export function addLatLng(poly: L.Polyline, latlng: L.LatLngExpression): L.Polyline {
    return poly.addLatLng(latlng);
}

export function setLatLngs(poly: L.Polyline, latlngs: L.LatLngExpression[]): L.Polyline {
    return poly.setLatLngs(latlngs);
}

export function getLatLngs(poly: L.Polyline): string {
    return JSON.stringify(poly.getLatLngs());
}

export function isEmpty(poly: L.Polyline): boolean {
    return poly.isEmpty();
}

export function closestLayerPoint(poly: L.Polyline, point: L.Point): string {
    return JSON.stringify(poly.closestLayerPoint(point));
}
