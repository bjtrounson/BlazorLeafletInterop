﻿using BlazorLeafletInterop.Models;
using BlazorLeafletInterop.Models.Options.Basic;
using Microsoft.JSInterop;

namespace BlazorLeafletInterop.Interops;

public interface IIconFactoryInterop
{
    ValueTask DisposeAsync();
    Task<IJSObjectReference> CreateIcon(IconOptions options);
    Task<IJSObjectReference> CreateDefaultIcon();
}