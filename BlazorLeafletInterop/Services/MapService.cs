using BlazorLeafletInterop.Factories;
using BlazorLeafletInterop.Interops;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorLeafletInterop.Services;

public static class MapService
{
    public static IServiceCollection AddMapService(this IServiceCollection services)
    {
        services.AddTransient<IBundleInterop, BundleInterop>();
        services.AddTransient<IIconFactoryInterop, IconFactoryInterop>();
        services.AddTransient<ILayerFactory, LayerFactory>();
        return services;
    }
}