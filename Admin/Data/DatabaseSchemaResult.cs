namespace Admin.Data;

public class DatabaseSchemaResult
{
    public string Table { get; set; }

    public List<SchemaQueryResult> Columns { get; set; }

    public DatabaseSchemaResult()
    {
        this.Columns = new List<SchemaQueryResult>();
    }
}

public class SchemaQueryResult
{
    public string Table { get; set; }
    public string Column { get; set; }
    public string Type { get; set; }
}