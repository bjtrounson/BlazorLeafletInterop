import * as L from "leaflet";
import * as geojson from "geojson";

export function createGeoJson(geoJson: any,
                        pointToLayer: (geoJsonPoint: geojson.Feature<geojson.Point, any>, latLng: L.LatLng) => L.Layer,
                        style: L.StyleFunction,
                        onEachFeature: (feature: geojson.Feature, layer: L.Layer) => void,
                        filter: (geoJsonFeature: geojson.Feature) => boolean,
                        markersInheritOptions: boolean): L.GeoJSON {
    return L.geoJSON(geoJson);
}