using BlazorShWebsite.Shared.Shared;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped<HubManager>();

await builder.Build().RunAsync();