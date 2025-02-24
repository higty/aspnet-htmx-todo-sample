using HigLabo.Web;
using AspnetHtmxTodoSample.Pages;
using AspnetHtmxTodoSample;
using System.Text.Json;
using HigLabo.DbSharp;
using HigLabo.Data;

var builder = WebApplication.CreateBuilder(args);
StoredProcedure.DatabaseFactory.SetCreateDatabaseMethod("DefaultDatabaseKey", () =>
{
    var json = File.ReadAllText("C:\\DevConfig\\aspnet-htmx-todo-sample-config.json");
    var config = JsonSerializer.Deserialize<ConfigData>(json)!;
    return new SqlServerDatabase(config.ConnectionString);
});

var sv = builder.Services;
sv.AddSingleton(provider =>
{
    var json = File.ReadAllText("C:\\DevConfig\\aspnet-htmx-todo-sample-config.json");
    var config = JsonSerializer.Deserialize<ConfigData>(json)!;
    return new SqlServerDatabaseFactory(config.ConnectionString);
});
sv.AddRazorPages();
sv.AddHttpContextAccessor();
sv.AddScoped<RazorRenderer>();
sv.AddSignalR();
sv.AddTransient<MyHub>();

var app = builder.Build();

var now = DateTime.Now;
var nowText = now.ToString();

app.UseStaticFiles();
app.MapTask();
app.MapHub<MyHub>("/MyHub");

app.Run();
