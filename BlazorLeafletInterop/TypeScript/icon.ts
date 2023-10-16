import * as L from 'leaflet';

export function createIcon(options: L.IconOptions): L.Icon {
    return L.icon(options);
}

export function createDefaultIcon(): L.Icon.Default {
    return new L.Icon.Default;
}