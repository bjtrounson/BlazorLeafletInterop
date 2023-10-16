import * as L from "leaflet";
import * as geojson from "geojson";

export function createGeoJson(geoJson: any,
                        pointToLayer: (geoJsonPoint: geojson.Feature<geojson.Point, any>, latLng: L.LatLng) => L.Layer | undefined | null,
                        style: L.StyleFunction | undefined | null,
                        onEachFeature: (feature: geojson.Feature, layer: L.Layer) => void | undefined | null,
                        filter: (geoJsonFeature: geojson.Feature) => boolean | undefined | null,
                        markersInheritOptions: boolean): L.GeoJSON {
    const options = {
        pointToLayer: pointToLayer ?? undefined,
        style: style ?? undefined,
        onEachFeature: onEachFeature ?? undefined,
        filter: filter ?? undefined,
        markersInheritOptions: markersInheritOptions
    };
    return L.geoJSON(geoJson);
}

export function addData(geoJson: L.GeoJSON, data: any) {
    geoJson.addData(data);
}