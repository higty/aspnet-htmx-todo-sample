using AspnetHtmxTodoSample.Data;

namespace AspnetHtmxTodoSample.Pages.View;

public class TodoListPanelModel
{
    public List<TodoRecord> TodoList { get; init; } = new();
}
