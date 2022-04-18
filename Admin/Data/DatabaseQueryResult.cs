namespace Admin.Data;

public class DatabaseQueryResult
{
    public List<string> Headers { get; set; }
    public List<List<string>> Rows { get; set; }
    public string ErrorMessage { get; set; }

    public DatabaseQueryResult()
    {
        this.Headers = new List<string>();
        this.Rows = new List<List<string>>();
    }
}