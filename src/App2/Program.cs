var builder = WebApplication.CreateBuilder(args);

builder.WebHost
    .UseKestrel(serverOptions => serverOptions
        .ListenUnixSocket(Path.Combine(Path.GetTempPath(), "app2.sock")));

var app = builder.Build();

app.MapGet("/", () => "Hello App2!");
app.MapGet("/test", () => "Test from App2!");

app.Run();
