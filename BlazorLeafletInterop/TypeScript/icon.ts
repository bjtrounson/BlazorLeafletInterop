import type * as L from "leaflet";

export function createDivIcon(options: L.DivIconOptions): L.DivIcon {
    // @ts-ignore
    return L.divIcon(options);
}

export function createIcon(options: L.IconOptions): L.Icon {
    // @ts-ignore
    return L.icon(options);
}

export function createDefaultIcon(): L.Icon.Default {
    // @ts-ignore
    return new L.Icon.Default;
}

