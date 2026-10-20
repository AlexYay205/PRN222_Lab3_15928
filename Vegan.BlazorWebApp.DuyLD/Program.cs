using Microsoft.EntityFrameworkCore;
using Vegan.BlazorWebApp.DuyLD.Components;
using Vegan.Entities.DuyLD.Models;
using Vegan.Services.DuyLD;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextFactory<VeganDBContext>();
builder.Services.AddQuickGridEntityFrameworkAdapter();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<IVeganPlaceServiceDuyLd, VeganPlaceServiceDuyLd>();
builder.Services.AddScoped<IPlaceFoodServiceDuyLd, PlaceFoodServiceDuyLd>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
