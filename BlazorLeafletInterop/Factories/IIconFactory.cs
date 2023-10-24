using BlazorLeafletInterop.Models.Options.Basic;
using Microsoft.JSInterop;

namespace BlazorLeafletInterop.Factories;

public interface IIconFactory
{
    Task<IJSObjectReference> CreateIcon(IconOptions options);
    Task<IJSObjectReference> CreateDefaultIcon();
}