using HigLabo.Data;

namespace AspnetHtmxTodoSample;

public class SqlServerDatabaseFactory
{
    public string ConnectionString { get; init; }
    public SqlServerDatabaseFactory(string connectionString)
    {
        this.ConnectionString = connectionString;
    }
    public SqlServerDatabase Create()
    {
        return new SqlServerDatabase(ConnectionString);
    }
}
