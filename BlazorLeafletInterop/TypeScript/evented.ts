export function on(layer: L.Evented, event: string, callback: (event: any) => void): L.Evented {
    return layer.on(event, callback);
}

export function on(): L.Evented {
    return null;
}

