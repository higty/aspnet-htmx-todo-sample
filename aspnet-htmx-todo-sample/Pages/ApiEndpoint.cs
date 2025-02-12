using HigLabo.Web;
using System.Text.Json;
using HigLabo.Data;
using Microsoft.Data.SqlClient;
using System.Reflection;
using AspnetHtmxTodoSample.Data;
using HigLabo.Core;
using AspnetHtmxTodoSample.Pages.View;

namespace AspnetHtmxTodoSample.Pages;

public static class ApiEndpoint
{
    public static void MapTask(this WebApplication app)
    {
        app.MapGet("/", View("/Pages/Home.cshtml"));
        app.MapGet("/task/list", PageTaskList);
        app.MapGet("/task/add", PageTaskAdd);
        app.MapGet("/task/{todoId:guid}/edit", PageTaskEdit);

        app.MapPost("/view/todo-list-panel", ViewTodoListPanel);

        app.MapPost("/api/task/add", ApiTaskAdd);
        app.MapPost("/api/task/edit", ApiTaskEdit);
        app.MapPost("/api/task/delete", ApiTaskDelete);
    }

    public class TaskListModel
    {
        public List<TodoRecord> TodoList { get; init; } = new();
    }
    public static async ValueTask PageTaskList(HttpContext context, RazorRenderer razorRenderer, SqlServerDatabaseFactory databaseFactory)
    {
        var model = new TaskListModel();
        await razorRenderer.WriteHtmlAsync(context, "/Pages/TaskList.cshtml", model);
    }
    public static async ValueTask PageTaskListByDataReader(HttpContext context, RazorRenderer razorRenderer, SqlServerDatabaseFactory databaseFactory)
    {
        var model = new TaskListModel();
        var db = databaseFactory.Create();
        var dr = await db.ExecuteReaderAsync("SELECT TodoId,Title,CreateTime,Priority,DueDate FROM Todo");
        while (dr.Read())
        {
            var r = new TodoRecord();
            r.TodoId = dr.GetGuid(0);
            r.Title = dr["Title"].ToString()!;
            r.CreateTime = (DateTimeOffset)dr["CreateTime"];
            r.Priority = (int)dr["Priority"];
            if (dr["DueDate"] != DBNull.Value)
            {
                r.DueDate = DateOnly.FromDateTime((DateTime)dr["DueDate"]);
            }
            model.TodoList.Add(r);
        }
        await razorRenderer.WriteHtmlAsync(context, "/Pages/TaskList.cshtml", model);
    }

    public class TaskEditModel(RecordEditMode editMode)
    {
        public RecordEditMode EditMode { get; init; } = editMode;
        public TodoRecord Record { get; set; } = new();
    }
    public static async ValueTask PageTaskAdd(HttpContext context, RazorRenderer razorRenderer)
    {
        var model = new TaskEditModel(RecordEditMode.Add);
        await razorRenderer.WriteHtmlAsync(context, "/Pages/TaskEdit.cshtml", model);
    }
    public static async ValueTask PageTaskEdit(HttpContext context, RazorRenderer razorRenderer, SqlServerDatabaseFactory databaseFactory, Guid todoId)
    {
        var model = new TaskEditModel(RecordEditMode.Edit);
        var sp = new Todo_Get_By_TodoId_20250210();
        sp.TodoId = todoId;
        var rs = await sp.GetFirstResultSetAsync();
        if (rs == null) { throw new InvalidOperationException("Record not found."); }
        var r = new TodoRecord();
        r.TodoId = rs.TodoId;
        r.Title = rs.Title;
        r.CreateTime = rs.CreateTime;
        r.DueDate = rs.DueDate;
        r.Priority = rs.Priority;   
        model.Record = r;
        await razorRenderer.WriteHtmlAsync(context, "/Pages/TaskEdit.cshtml", model);
    }

    public class ViewTodoListPanelParameter
    {
        public bool? DueDateNotNull { get; set; }
    }
    public static async ValueTask ViewTodoListPanel(HttpContext context, RazorRenderer razorRenderer, SqlServerDatabaseFactory databaseFactory)
    {
        var p = await context.CreateFromBody<ViewTodoListPanelParameter>();

        var model = new TodoListPanelModel();
        var sp = new Todo_Get_20250210();
        sp.DueDateNotNull = p.DueDateNotNull;
        foreach (var item in await sp.GetResultSetsAsync())
        {
            var r = new TodoRecord();
            r.TodoId = item.TodoId;
            r.Title = item.Title;
            r.CreateTime = item.CreateTime;
            r.Priority = item.Priority;
            r.DueDate = item.DueDate;
            model.TodoList.Add(r);
        }
        await razorRenderer.WriteHtmlAsync(context, "/Pages/View/TodoListPanel.cshtml", model);
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

        if (p.Title.IsNullOrEmpty())
        {
            await context.Response.WriteAsync("<div hx-swap-oob=\"innerHTML:[validate-result-panel='Title']\">Title is required!</div>");
            return;
        }

        var sp = new Data.Todo_Add_20250210();
        sp.TodoId = Guid.NewGuid();
        sp.Title = p.Title;
        sp.CreateTime = DateTimeOffset.Now;
        sp.Priority = p.Priority;
        sp.DueDate = p.DueDate;
        var result = await sp.ExecuteNonQueryAsync();

        context.Response.Headers.TryAdd("HX-Redirect", "/task/list");
    }
    public class ApiTaskEditParameter
    {
        public Guid TodoId { get; set; }
        public string Title { get; set; } = "";
        public int? Priority { get; set; }
        public DateOnly? DueDate { get; set; }
    }
    public static async ValueTask ApiTaskEdit(HttpContext context, SqlServerDatabaseFactory databaseFactory)
    {
        var p = await context.CreateFromBody<ApiTaskEditParameter>();
        if (p.Title.IsNullOrEmpty())
        {
            await context.Response.WriteAsync("<div hx-swap-oob=\"innerHTML:[validate-result-panel='Title']\">Title is required!</div>");
            await context.Response.WriteAsync("Title is required!");
            return;
        }

        var sp = new Data.Todo_Edit_20250210();
        sp.TodoId = p.TodoId;
        sp.Title = p.Title;
        sp.Priority = p.Priority;
        sp.DueDate = p.DueDate;
        var result = await sp.ExecuteNonQueryAsync();

        context.Response.Headers.TryAdd("HX-Redirect", "/task/list");
    }
    public class ApiTaskDeleteParameter
    {
        public Guid TodoId { get; set; }
    }
    public static async ValueTask ApiTaskDelete(HttpContext context, SqlServerDatabaseFactory databaseFactory)
    {
        var p = await context.CreateFromBody<ApiTaskDeleteParameter>();

        var sp = new Data.Todo_Delete_20250210();
        sp.TodoId = p.TodoId;
        var result = await sp.ExecuteNonQueryAsync();

        context.Response.Headers.TryAdd("HX-Redirect", "/task/list");
    }

    public static Func<HttpContext, RazorRenderer, Task> View(string viewName)
    {
        var f = async (HttpContext context, RazorRenderer razorRenderer) => await razorRenderer.WriteHtmlAsync(context, viewName);
        return f;
    }
}
