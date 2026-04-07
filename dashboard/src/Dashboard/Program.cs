using Dashboard.Components;
using Dashboard.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMemoryCache();
builder.Services.AddHttpClient();

builder.Services.AddSingleton<AppInsightsService>();
builder.Services.AddSingleton<YouTubeAnalyticsService>();

var app = builder.Build();

// Log configuration status at startup for deployment diagnostics
var appInsights = app.Services.GetRequiredService<AppInsightsService>();
appInsights.LogConfigurationStatus();

var youtube = app.Services.GetRequiredService<YouTubeAnalyticsService>();
app.Logger.LogInformation("YouTube API configured: {IsConfigured}", youtube.IsConfigured);

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
