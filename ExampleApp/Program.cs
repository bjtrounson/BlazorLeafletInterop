using System.Runtime.InteropServices.JavaScript;
using BlazorLeafletInterop;
using BlazorLeafletInterop.Interops;
using BlazorLeafletInterop.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ExampleApp;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMapService();

var app = builder.Build();
await app.RunAsync();
