using BlazorShWebsite.Client.Js;
using BlazorShWebsite.Server.Components;
using Notes = BlazorShWebsite.Notes;
using SharedNotes = BlazorShWebsite.Shared.Notes;
using BlazorShWebsite.Shared.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddSignalR();

builder.Services.AddScoped<HubManager>();
builder.Services.AddJsInterop();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BlazorShWebsite.Client._Imports).Assembly);

app.MapHub<Notes.Hub>(SharedNotes.Constants.PartialHubPath.ToHubUrl());

app.Run();