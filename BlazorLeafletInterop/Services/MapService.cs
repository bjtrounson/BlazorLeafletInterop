using BlazorLeafletInterop.Factories;
using BlazorLeafletInterop.Interops;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorLeafletInterop.Services;

public static class MapService
{
    public static IServiceCollection AddMapService(this IServiceCollection services)
    {
        services.AddSingleton<IBundleInterop, BundleInterop>();
        services.AddSingleton<IIconFactoryInterop, IconFactoryInterop>();
        services.AddSingleton<ILayerFactory, LayerFactory>();
        return services;
    }
}