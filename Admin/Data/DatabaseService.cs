namespace Admin.Data;
using Npgsql;

public class DatabaseService
{
    public async Task<DatabaseQueryResult> FromRawSqlAsync(string query)
    {
        string constr = Environment.GetEnvironmentVariable("CONNECTION_STRING");
        DatabaseQueryResult results = new DatabaseQueryResult();
        using (NpgsqlConnection con = new NpgsqlConnection(constr))
        {
            using (NpgsqlCommand cmd = new NpgsqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();
                try
                {
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
                }
                catch (System.AggregateException ex)
                {
                    results.ErrorMessage = ex.Message;
                }
                con.Close();
            }
        }
        return results;
    }

    public async Task<List<DatabaseSchemaResult>> GetDatabaseSchema()
    {
        string schema = Environment.GetEnvironmentVariable("SCHEMA");

        string query = @"SELECT t.table_name, c.column_name, c.data_type FROM information_schema.tables as t 
                        JOIN information_schema.columns as c on t.table_name = c.table_name
                        WHERE t.table_schema = '" + schema + "';";

        string constr = Environment.GetEnvironmentVariable("CONNECTION_STRING");

        List<SchemaQueryResult> results = new List<SchemaQueryResult>();

        using (NpgsqlConnection con = new NpgsqlConnection(constr))
        {
            using (NpgsqlCommand cmd = new NpgsqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();
                using (NpgsqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        results.Add(new SchemaQueryResult()
                        {
                            Table = sdr["table_name"].ToString(),
                            Column = sdr["column_name"].ToString(),
                            Type = sdr["data_type"].ToString()
                        });
                    }
                }
                con.Close();
            }
        }

        List<DatabaseSchemaResult> groupedSchema = results.GroupBy(u => u.Table)
                                    .Select(grp => new DatabaseSchemaResult
                                    {
                                        Table = grp.Key,
                                        Columns = grp.ToList()
                                    }).ToList();

        return groupedSchema;
    }
}