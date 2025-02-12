namespace AspnetHtmxTodoSample.Data;

public class TodoRecord
{
    public Guid TodoId { get; set; }
    public string Title { get; set; } = "";
    public DateTimeOffset CreateTime { get; set; }
    public int Priority { get; set; }
    public DateOnly? DueDate { get; set; }
}
