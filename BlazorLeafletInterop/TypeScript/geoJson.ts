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
    if (!disablePointToLayer) options.pointToLayer = (feature, latLng) => {
        let featureString = JSON.stringify(feature);
        let latLngString = JSON.stringify(latLng);
        return instance.invokeMethod("PointToLayerCallback", featureString, latLngString);
    }
    if (!disableStyle) options.style = (feature) => {
        let featureString = JSON.stringify(feature);
        return instance.invokeMethod("StyleCallback", featureString);
    }
    if (!disableOnEachFeature) options.onEachFeature = (feature, layer) => {
        let featureString = JSON.stringify(feature);
        instance.invokeMethod("OnEachFeatureCallback", featureString, DotNet.createJSObjectReference(layer));
    }
    if (!disableFilter) options.filter = (geoJsonFeature) => {
        let featureString = JSON.stringify(geoJsonFeature);
        return instance.invokeMethod("FilterCallback", featureString);
    }
    options.markersInheritOptions = markersInheritOptions;
    // @ts-ignore
    return L.geoJSON(geoJson, options);
}

export function addData(geoJson: L.GeoJSON, data: any) {
    geoJson.addData(data);
}