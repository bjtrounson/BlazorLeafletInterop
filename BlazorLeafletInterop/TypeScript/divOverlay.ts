import type * as L from "leaflet";

export function openOn(divOverlay: L.DivOverlay, map: L.Map): L.DivOverlay {
    return divOverlay.openOn(map);
}

export function close(divOverlay: L.DivOverlay): L.DivOverlay {
    return divOverlay.close();
}

export function toggle(divOverlay: L.DivOverlay, layer: L.Layer | undefined | null): L.DivOverlay {
    return divOverlay.toggle(layer);
}

export function getContent(divOverlay: L.DivOverlay): string {
    const content = divOverlay.getContent();
    return content.toString();
}

export function setContent(divOverlay: L.DivOverlay, htmlContent: HTMLElement | string): L.DivOverlay {
    return divOverlay.setContent(htmlContent);
}

export function getElement(divOverlay: L.DivOverlay): HTMLElement {
    return divOverlay.getElement();
}

export function update(divOverlay: L.DivOverlay): void {
    divOverlay.update();
}

export function isOpen(divOverlay: L.DivOverlay): boolean {
    return divOverlay.isOpen();
}
