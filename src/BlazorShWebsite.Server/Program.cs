using BlazorShWebsite.Client.Js;
using BlazorShWebsite.Server.Components;
using BlazorShWebsite.Server.Services;
using Notes = BlazorShWebsite.Notes;
using SharedNotes = BlazorShWebsite.Shared.Notes;
using BlazorShWebsite.Shared.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddSignalR();

builder.Services.AddScoped<HubManager>();
builder.Services.AddJsInterop();

var app = builder.Build();

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/error", createScopeForErrors: true);

    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/error", "?statusCode={0}");

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BlazorShWebsite.Client._Imports).Assembly);

app.MapGet("/sitemap.xml", Sitemap.Generate);

app.MapHub<Notes.Hub>(SharedNotes.Constants.PartialHubPath.ToHubUrl());

app.Run();