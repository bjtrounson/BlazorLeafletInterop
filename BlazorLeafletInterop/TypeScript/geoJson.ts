import type * as L from "leaflet";
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
    if (!disablePointToLayer) options.pointToLayer = (...args) => instance.invokeMethod("PointToLayerCallback", ...args);
    if (!disableStyle) options.style = (...args) => instance.invokeMethod("StyleCallback", ...args);
    if (!disableOnEachFeature) options.onEachFeature = (...args) => instance.invokeMethod("OnEachFeatureCallback", ...args);
    if (!disableFilter) options.filter = (...args) => instance.invokeMethod("FilterCallback", ...args);
    options.markersInheritOptions = markersInheritOptions;
    // @ts-ignore
    return L.geoJSON(geoJson, options);
}

export function addData(geoJson: L.GeoJSON, data: any) {
    geoJson.addData(data);
}