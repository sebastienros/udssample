var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello App1!");
app.MapGet("/test", () => "Test from App1!");

app.Run();
