import * as L from 'leaflet';

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

export function on(layer: L.Evented, event: string, callback: (event: any) => void): L.Evented {
    return layer.on(event, callback);
}

export function off(layer: L.Evented, event: string | undefined | null, callback: (event: any) => void | undefined | null): L.Evented {
    if (event === undefined || event === null) {
        return layer.off();
    }
    return layer.off(event, callback);
}

export function once(layer: L.Evented, event: string, callback: (event: any) => void): L.Evented {
    return layer.once(event, callback);
}

export function fire(layer: L.Evented, event: string, data: any): L.Evented {
    return layer.fire(event, data);
}

export function addEventListener(layer: L.Evented, event: string, callback: (event: any) => void): L.Evented {
    return layer.addEventListener(event, callback);
}

export function removeEventListener(layer: L.Evented, event: string, callback: (event: any) => void): L.Evented {
    return layer.removeEventListener(event, callback);
}

export function clearAllEventListeners(layer: L.Evented): L.Evented {
    return layer.clearAllEventListeners();
}

export function addOneTimeEventListener(layer: L.Evented, event: string, callback: (event: any) => void): L.Evented {
    return layer.addOneTimeEventListener(event, callback);
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




