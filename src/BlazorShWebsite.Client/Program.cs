using BlazorShWebsite.Client.Js;
using BlazorShWebsite.Shared.Shared;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped<HubManager>();
builder.Services.AddJsInterop();

await builder.Build().RunAsync();