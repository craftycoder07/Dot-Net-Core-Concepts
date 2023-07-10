using HttpClientUsage;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

//For time being using this way. It's good to have extension method as suggested in 'Configuration' module
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile($"appsettings.Development.json", optional: false, reloadOnChange: true);

builder.Services.Configure<HerokuSettings>(builder.Configuration.GetSection("HerokuSettings"));
builder.Services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<HerokuSettings>>().Value);

//Register http client with the container
builder.Services.AddHttpClient<HerokuBookingAppClient>();

var app = builder.Build();

app.MapGet("/bookings", async (HerokuBookingAppClient herokuBookingAppClient) => {
    
    //Utilize http client in endpoint by injecting it.
    return await herokuBookingAppClient.GetBookings();
});

app.Run();
