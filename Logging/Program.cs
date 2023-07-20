
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

logger.Information("Starting server");

app.MapGet("/", () => "Hello World!");

app.Run();
