import type * as L from "leaflet";
import DotNetObject = DotNet.DotNetObject;

type Events = 
    "baselayerchange"
    | "overlayadd"
    | "overlayremove"
    | "layeradd"
    | "layerremove"
    | "zoomlevelschange"
    | "unload"
    | "viewreset"
    | "load"
    | "zoomstart"
    | "movestart"
    | "zoom"
    | "move"
    | "zoomend"
    | "moveend"
    | "autopanstart"
    | "dragstart"
    | "drag"
    | "add"
    | "remove"
    | "loading"
    | "error"
    | "update"
    | "down"
    | "predrag"
    | "resize"
    | "popupopen"
    | "tooltipopen"
    | "tooltipclose"
    | "locationerror"
    | "locationfound"
    | "click"
    | "dblclick"
    | "mousedown"
    | "mouseup"
    | "mouseover"
    | "mouseout"
    | "mousemove"
    | "contextmenu"
    | "preclick"
    | "keypress"
    | "keydown"
    | "keyup"
    | "zoomanim"
    | "dragend"
    | "tileunload"
    | "tileloadstart"
    | "tileload"
    | "tileabort"
    | "tileerror";

export function on(instance: DotNetObject, layer: L.Evented, event: string): L.Evented {
    return layer.on(event, (e: any) => instance.invokeMethodAsync("OnEventCallback", e));
}

export function off(instance: DotNetObject, layer: L.Evented, event: string | undefined | null): L.Evented {
    if (event === undefined || event === null) {
        return layer.off();
    }
    return layer.off(event, (e: any) => instance.invokeMethodAsync("OffEventCallback", e));
}

export function once(instance: DotNetObject, layer: L.Evented, event: string): L.Evented {
    return layer.once(event, (e: any) => instance.invokeMethodAsync("OnceEventCallback", e));
}

export function fire(layer: L.Evented, event: string, data: any): L.Evented {
    return layer.fire(event, data);
}

export function addEventListener(instance: DotNetObject, layer: L.Evented, event: string): L.Evented {
    return layer.addEventListener(event, (e: any) => instance.invokeMethodAsync("AddEventListenerEventCallback", e));
}

export function removeEventListener(instance: DotNetObject, layer: L.Evented, event: string): L.Evented {
    return layer.removeEventListener(event, (e: any) => instance.invokeMethodAsync("RemoveEventListenerEventCallback", e));
}

export function clearAllEventListeners(layer: L.Evented): L.Evented {
    return layer.clearAllEventListeners();
}

export function addOneTimeEventListener(instance: DotNetObject, layer: L.Evented, event: string): L.Evented {
    return layer.addOneTimeEventListener(event, (e: any) => instance.invokeMethodAsync("AddOneTimeEventListenerEventCallback", e));
}

export function fireEvent(layer: L.Evented, event: string, data: any): L.Evented {
    return layer.fireEvent(event, data);
}

export function hasEventListeners(layer: L.Evented, event: string): boolean {
    return layer.hasEventListeners(event);
}

export function listens(layer: L.Evented, event: Events): boolean {
    return layer.listens(event);
}




