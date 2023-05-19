using Microsoft.Extensions.Options;
using MinimalApi;

var builder = WebApplication.CreateBuilder(args);

//Binding using IOptions pattern
builder.Services.Configure<OptionsPatternSettings>(builder.Configuration.GetSection("OptionsPatternSettings"));

//Avoiding IOption dependency in constructor
builder.Services.Configure<AvoidOptionsPatternSettings>(builder.Configuration.GetSection("AvoidOptionsPatternSettings"));
builder.Services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<AvoidOptionsPatternSettings>>().Value);

var app = builder.Build();

//Binding using IOptions pattern
app.MapGet("/OptionsPattern", (IOptions<OptionsPatternSettings> options) =>
{
    OptionsPatternSettings optionsPatternSettings = options.Value;
    if(optionsPatternSettings.PrintSpecializedMessage)
        return "Hello Sid";
    else
        return "Hello all";
});

//Avoiding IOption dependency in constructor
app.MapGet("/AvoidOptionsPattern", (AvoidOptionsPatternSettings avoidOptionsPatternSettings) =>
{
    if(avoidOptionsPatternSettings.PrintSpecializedMessage)
        return "Hello Sid";
    else
        return "Hello all";
});

app.Run();
