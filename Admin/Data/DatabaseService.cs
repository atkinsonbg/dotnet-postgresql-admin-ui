namespace Admin.Data;
using Npgsql;

public class DatabaseService
{
    public async Task<List<List<string>>> FromRawSqlAsync(string query)
    {
        string constr = "Server=127.0.0.1;Port=5432;Database=postgres;User Id=postgres;";
        List<List<string>> results = new List<List<string>>();
        using (NpgsqlConnection con = new NpgsqlConnection(constr))
        {
            using (NpgsqlCommand cmd = new NpgsqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();
                using (NpgsqlDataReader sdr = cmd.ExecuteReader())
                {
                    int fieldCount = sdr.FieldCount - 1;
                    var x = sdr.GetColumnSchema();

                    List<string> colNames = new List<string>();
                    foreach (var colName in x)
                    {
                        colNames.Add(colName.ColumnName);
                    }
                    results.Add(colNames);


                    while (sdr.Read())
                    {
                        List<string> colValues = new List<string>();

                        for (int i = 0; i <= fieldCount; i++)
                        {
                            colValues.Add(sdr[i].ToString());
                        }
                        results.Add(colValues);
                    }
                }
                con.Close();
            }
        }
        return results;
    }
}