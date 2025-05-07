using Microsoft.Data;
using Microsoft.Data.SqlClient;
namespace DatabaseComparer;
public class DatabaseService
{
    private readonly string _connectionString;

    public DatabaseService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<System.Data.DataTable> GetTableDataAsync(string tableName)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var cmd = new SqlCommand($"SELECT * FROM {tableName}", conn);
        var adapter = new SqlDataAdapter(cmd);
        var dt = new System.Data.DataTable();
        adapter.Fill(dt);
        return dt;
    }
}
