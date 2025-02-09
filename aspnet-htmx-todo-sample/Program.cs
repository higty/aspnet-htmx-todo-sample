var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var now = DateTime.Now;
var nowText = now.ToString();

app.MapGet("/", () => "Hello World!");

app.Run();
