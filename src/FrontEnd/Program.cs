using FrontEnd;
using Yarp.ReverseProxy.Forwarder;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddSingleton<IForwarderHttpClientFactory, UdsHttpClientFactory>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

// Register the reverse proxy routes
app.MapReverseProxy();

app.Run();
