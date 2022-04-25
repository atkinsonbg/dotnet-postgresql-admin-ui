using System.Dynamic;

namespace Admin.Data;

public class DatabaseQueryResult
{
    public Dictionary<string, object> Headers { get; set; }
    public TableQueryResult Result { get; set; }
    public string ErrorMessage { get; set; }

    public DatabaseQueryResult()
    {
        this.Result = new TableQueryResult();
        this.Headers = new Dictionary<string, object>();
    }
}

public class TableQueryResult
{
    public int TotalCount
    {
        get
        {
            return this.Rows.Count();
        }
    }
    public List<ExpandoObject> Rows { get; set; }

    public TableQueryResult()
    {
        this.Rows = new List<ExpandoObject>();
    }
}