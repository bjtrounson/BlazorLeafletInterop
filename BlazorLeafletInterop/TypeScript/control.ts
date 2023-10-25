import type * as L from "leaflet";
import DotNetObject = DotNet.DotNetObject;

export function getPosition(control: L.Control): string {
    return control.getPosition();
}

export function setPosition(control: L.Control, position: L.ControlPosition): L.Control {
    return control.setPosition(position);
}

export function setPrefix(control: L.Control.Attribution, prefix: string): L.Control {
    return control.setPrefix(prefix);
}

export function addAttribution(control: L.Control.Attribution, text: string): L.Control {
    return control.addAttribution(text);
}

export function removeAttribution(control: L.Control.Attribution, text: string): L.Control {
    return control.removeAttribution(text);
}

export function addBaseLayer(control: L.Control.Layers, layer: L.Layer, name: string): L.Control {
    return control.addBaseLayer(layer, name);
}

export function addOverlay(control: L.Control.Layers, layer: L.Layer, name: string): L.Control {
    return control.addOverlay(layer, name);
}

export function expand(control: L.Control.Layers): L.Control {
    return control.expand();
}

export function collapse(control: L.Control.Layers): L.Control {
    return control.collapse();
}

export function createZoom(options: L.Control.ZoomOptions): L.Control.Zoom {
    // @ts-ignore
    return L.control.zoom(options);
}

export function createAttribution(options: L.Control.AttributionOptions): L.Control.Attribution {
    // @ts-ignore
    return L.control.attribution(options);
}

export function createLayers(baseLayers: any, overlays: any, options: L.Control.LayersOptions): L.Control.Layers {
    // @ts-ignore
    return L.control.layers(baseLayers, overlays, options);
}

export function createScale(options: L.Control.ScaleOptions): L.Control.Scale {
    // @ts-ignore
    return L.control.scale(options);
}