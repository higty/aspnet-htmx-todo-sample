using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;

namespace AspnetHtmxTodoSample.Data.Query.StoredProcedure;

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
        var cm = new SqlCommand("Todo_Add_20250210");
        cm.CommandType = System.Data.CommandType.StoredProcedure;
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
