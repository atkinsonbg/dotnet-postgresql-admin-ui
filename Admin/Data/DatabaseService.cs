namespace Admin.Data;
using Npgsql;

public class DatabaseService
{
    public async Task<DatabaseQueryResult> FromRawSqlAsync(string query)
    {
        string constr = "Server=127.0.0.1;Port=5432;Database=postgres;User Id=postgres;";
        DatabaseQueryResult results = new DatabaseQueryResult();
        using (NpgsqlConnection con = new NpgsqlConnection(constr))
        {
            using (NpgsqlCommand cmd = new NpgsqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();
                using (NpgsqlDataReader sdr = cmd.ExecuteReader())
                {
                    int fieldCount = sdr.FieldCount - 1;
                    var schema = sdr.GetColumnSchema();

                    foreach (var s in schema)
                    {
                        results.Headers.Add(s.ColumnName);
                    }

                    while (sdr.Read())
                    {
                        List<string> colValues = new List<string>();

                        for (int i = 0; i <= fieldCount; i++)
                        {
                            colValues.Add(sdr[i].ToString());
                        }
                        results.Rows.Add(colValues);
                    }
                }
                con.Close();
            }
        }
        return results;
    }
}