using HigLabo.Web;
using System.Text.Json;
using HigLabo.Data;
using Microsoft.Data.SqlClient;

namespace AspnetHtmxTodoSample.Pages;

public static class ApiEndpoint
{
    public static void MapTask(this WebApplication app)
    {
        app.MapGet("/", View("/Pages/Home.cshtml"));
        app.MapGet("/task/list", View("/Pages/TaskList.cshtml"));
        app.MapGet("/task/add", View("/Pages/TaskAdd.cshtml"));

        app.MapPost("/api/task/add", ApiTaskAdd);
    }

    public class ApiTaskAddParameter
    {
        public string Title { get; set; } = "";
        public int? Priority { get; set; }
        public DateOnly? DueDate { get; set; }
    }
    public static async ValueTask ApiTaskAdd(HttpContext context, SqlServerDatabaseFactory databaseFactory)
    {
        var p = await context.CreateFromBody<ApiTaskAddParameter>();

        var sp = new Data.Todo_Add_20250210();
        sp.TodoId = Guid.NewGuid();
        sp.Title = p.Title;
        sp.CreateTime = DateTimeOffset.Now;
        sp.Priority = p.Priority;
        sp.DueDate = p.DueDate;
        var result = await sp.ExecuteNonQueryAsync();
    }

    public static Func<HttpContext, RazorRenderer, Task> View(string viewName)
    {
        var f = async (HttpContext context, RazorRenderer razorRenderer) => await razorRenderer.WriteHtmlAsync(context, viewName);
        return f;
    }
}
