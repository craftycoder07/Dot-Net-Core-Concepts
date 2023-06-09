using Microsoft.Extensions.Options;
using MinimalApi;

var builder = WebApplication.CreateBuilder(args);

builder.Host.SetupAppConfiguration();

//Binding using IOptions pattern
builder.Services.Configure<OptionsPatternSettings>(builder.Configuration.GetSection("OptionsPatternSettings"));

//Avoiding IOption dependency in constructor
builder.Services.Configure<AvoidOptionsPatternSettings>(builder.Configuration.GetSection("AvoidOptionsPatternSettings"));
builder.Services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<AvoidOptionsPatternSettings>>().Value);

var app = builder.Build();

// POINT 1:
// Binding using IOptions pattern
app.MapGet("/OptionsPattern", (IOptions<OptionsPatternSettings> options) =>
{
    OptionsPatternSettings optionsPatternSettings = options.Value;
    return optionsPatternSettings.SpecializedMessage;
});

// POINT 2:
// Avoiding IOption dependency in constructor.
// Fetching settings from appsettings.json based on environment
app.MapGet("/AvoidOptionsPattern", (AvoidOptionsPatternSettings avoidOptionsPatternSettings) =>
{
    return avoidOptionsPatternSettings.SpecializedMessage;
});

app.Run();
