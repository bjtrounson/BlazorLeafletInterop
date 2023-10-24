using BlazorLeafletInterop.Factories;
using BlazorLeafletInterop.Interops;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorLeafletInterop.Services;

public static class MapService
{
    public static IServiceCollection AddMapService(this IServiceCollection services)
    {
        services.AddSingleton<IBundleInterop, BundleInterop>();
        services.AddSingleton<ILayerFactory, LayerFactory>();
        services.AddSingleton<IIconFactory, IconFactory>();
        return services;
    }
}