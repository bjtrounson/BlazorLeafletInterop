import type * as L from "leaflet";
export function createMarkerClusterGroup(options: L.MarkerClusterGroupOptions): L.MarkerClusterGroup {
    // @ts-ignore
    return L.markerClusterGroup(options);
}