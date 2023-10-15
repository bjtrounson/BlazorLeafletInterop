import * as L from 'leaflet';

export function addTo(map: L.Map | L.LayerGroup, layer: L.Layer): L.Layer {
    return layer.addTo(map);
}

export function remove(layer: L.Layer): L.Layer {
    return layer.remove();
}

export function removeFrom(map: L.Map | L.LayerGroup, layer: L.Layer): L.Layer {
    if (map instanceof L.LayerGroup) return map.removeLayer(layer);
    return layer.removeFrom(map);
}

export function getPane(map: L.Map | L.LayerGroup, name: string): HTMLElement | undefined {
    return map.getPane(name);
}

export function getAttribution(layer: L.Layer): string | null {
    return layer.getAttribution();
}

export function bindPopup(layer: L.Layer, content: string | HTMLElement, options?: L.PopupOptions): L.Layer {
    return layer.bindPopup(content, options);
}

export function unbindPopup(layer: L.Layer): L.Layer {
    return layer.unbindPopup();
}

export function openPopup(layer: L.Layer, latlng?: L.LatLngExpression): L.Layer {
    return layer.openPopup(latlng);
}

export function closePopup(layer: L.Layer): L.Layer {
    return layer.closePopup();
}

export function togglePopup(layer: L.Layer): L.Layer {
    return layer.togglePopup();
}

export function isPopupOpen(layer: L.Layer): boolean {
    return layer.isPopupOpen();
}

export function setPopupContent(layer: L.Layer, content: string | HTMLElement): L.Layer {
    return layer.setPopupContent(content);
}

export function getPopup(layer: L.Layer): L.Popup | undefined {
    return layer.getPopup();
}

export function bindTooltip(layer: L.Layer, content: string | HTMLElement, options?: L.TooltipOptions): L.Layer {
    return layer.bindTooltip(content, options);
}

export function unbindTooltip(layer: L.Layer): L.Layer {
    return layer.unbindTooltip();
}

export function openTooltip(layer: L.Layer, latlng?: L.LatLngExpression): L.Layer {
    return layer.openTooltip(latlng);
}

export function closeTooltip(layer: L.Layer): L.Layer {
    return layer.closeTooltip();
}

export function toggleTooltip(layer: L.Layer): L.Layer {
    return layer.toggleTooltip();
}

export function isTooltipOpen(layer: L.Layer): boolean {
    return layer.isTooltipOpen();
}

export function setTooltipContent(layer: L.Layer, content: string | HTMLElement): L.Layer {
    return layer.setTooltipContent(content);
}

export function getTooltip(layer: L.Layer): L.Tooltip | undefined {
    return layer.getTooltip();
}