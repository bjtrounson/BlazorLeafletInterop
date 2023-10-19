import * as L from "leaflet";
import * as geojson from "geojson";
import DotNetObject = DotNet.DotNetObject;
export function createGeoJson(
    instance: DotNetObject,
    geoJson: any,
    markersInheritOptions: boolean,
    disablePointToLayer: boolean,
    disableStyle: boolean,
    disableOnEachFeature: boolean,
    disableFilter: boolean,
): L.GeoJSON {
    // Check if option has a string of "disabled" if so, then remove it from the options object
    const options: L.GeoJSONOptions = {};
    if (!disablePointToLayer) options.pointToLayer = instance.invokeMethod("PointToLayerCallback");
    if (!disableStyle) options.style = instance.invokeMethod("StyleCallback");
    if (!disableOnEachFeature) options.onEachFeature = instance.invokeMethod("OnEachFeatureCallback");
    if (!disableFilter) options.filter = instance.invokeMethod("FilterCallback");
    options.markersInheritOptions = markersInheritOptions;
    return L.geoJSON(geoJson, options);
}

export function addData(geoJson: L.GeoJSON, data: any) {
    geoJson.addData(data);
}