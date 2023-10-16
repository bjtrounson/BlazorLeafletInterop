import * as L from "leaflet";
import * as geojson from "geojson";

type PointToLayerFn = (geoJsonPoint: geojson.Feature<geojson.Point, any>, latLng: L.LatLng) => L.Layer;
type StyleFn = L.StyleFunction;
type OnEachFeatureFn = (feature: geojson.Feature, layer: L.Layer) => void;
type FilterFn = (geoJsonFeature: geojson.Feature) => boolean;
export function createGeoJson(
    geoJson: any,
    markersInheritOptions: boolean,
    disablePointToLayer: boolean,
    disableStyle: boolean,
    disableOnEachFeature: boolean,
    disableFilter: boolean,
    pointToLayer: PointToLayerFn,
    style: StyleFn,
    onEachFeature: OnEachFeatureFn,
    filter: FilterFn
): L.GeoJSON {
    // Check if option has a string of "disabled" if so, then remove it from the options object
    const options: L.GeoJSONOptions = {};
    if (!disablePointToLayer) options.pointToLayer = pointToLayer;
    if (!disableStyle) options.style = style;
    if (!disableOnEachFeature) options.onEachFeature = onEachFeature;
    if (!disableFilter) options.filter = filter;
    options.markersInheritOptions = markersInheritOptions;
    console.log(options);
    return L.geoJSON(geoJson, options);
}

export function addData(geoJson: L.GeoJSON, data: any) {
    geoJson.addData(data);
}