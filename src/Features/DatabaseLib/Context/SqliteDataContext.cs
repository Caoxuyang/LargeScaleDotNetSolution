using Microsoft.Data.Sqlite;
using LargeScaleSolution.Abstractions;

namespace LargeScaleSolution.DatabaseLib.Context;

public sealed class SqliteDataContext : IDataContext
{
    private readonly string _connectionString;
    private SqliteConnection? _connection;
    private bool _disposed;

    public SqliteDataContext(string connectionString)
    {
        _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
    }

    public async Task<SqliteConnection> GetConnectionAsync()
    {
        if (_connection == null)
        {
            _connection = new SqliteConnection(_connectionString);
            await _connection.OpenAsync();
        }
        return _connection;
    }

    public async Task ExecuteAsync(string sql, object? parameters = null)
    {
        var connection = await GetConnectionAsync();
        using var command = connection.CreateCommand();
        command.CommandText = sql;
        await command.ExecuteNonQueryAsync();
    }

    public async Task<T?> QueryFirstOrDefaultAsync<T>(string sql, object? parameters = null)
    {
        var connection = await GetConnectionAsync();
        using var command = connection.CreateCommand();
        command.CommandText = sql;

        var result = await command.ExecuteScalarAsync();
        if (result == null || result == DBNull.Value)
            return default;

        return (T)Convert.ChangeType(result, typeof(T));
    }

    public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? parameters = null)
    {
        var connection = await GetConnectionAsync();
        using var command = connection.CreateCommand();
        command.CommandText = sql;

        var results = new List<T>();
        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            if (typeof(T) == typeof(string))
            {
                results.Add((T)(object)reader.GetString(0));
            }
            else if (typeof(T) == typeof(int))
            {
                results.Add((T)(object)reader.GetInt32(0));
            }
        }

        return results;
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            _connection?.Dispose();
            _disposed = true;
        }
    }
}
