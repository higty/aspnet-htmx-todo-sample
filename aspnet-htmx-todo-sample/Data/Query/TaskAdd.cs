using Microsoft.Data.SqlClient;

namespace AspnetHtmxTodoSample.Data.Query;

public class TaskAdd
{
    public string Title { get; set; } = "";
    public int? Priority { get; set; }
    public DateOnly? DueDate { get; set; }

    public TaskAdd() { }
    public TaskAdd(string title, int? priority, DateOnly? dueDate)
    {
        Title = title;
        Priority = priority;
        DueDate = dueDate;
    }
    public SqlCommand CreateCommand()
    {
        var cm = new SqlCommand("INSERT Todo(TodoId,Title,CreateTime,Priority,DueDate)VALUES(@TodoId,@Title,@CreateTime,@Priority,@DueDate)");
        cm.Parameters.Add("@TodoId", System.Data.SqlDbType.UniqueIdentifier);
        cm.Parameters.Add("@Title", System.Data.SqlDbType.NVarChar, 128);
        cm.Parameters.Add("@CreateTime", System.Data.SqlDbType.DateTimeOffset);
        cm.Parameters.Add("@Priority", System.Data.SqlDbType.Int);
        cm.Parameters.Add("@DueDate", System.Data.SqlDbType.Date);

        cm.Parameters["@TodoId"].Value = Guid.NewGuid();
        cm.Parameters["@Title"].Value = this.Title;
        cm.Parameters["@CreateTime"].Value = DateTimeOffset.Now;
        cm.Parameters["@Priority"].Value = this.Priority ?? 3;
        cm.Parameters["@DueDate"].Value = this.DueDate;

        return cm;
    }

}
