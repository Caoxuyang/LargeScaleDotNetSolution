namespace LargeScaleSolution.Abstractions;

public interface IDataContext : IDisposable
{
    Task ExecuteAsync(string sql, object? parameters = null);
    Task<T?> QueryFirstOrDefaultAsync<T>(string sql, object? parameters = null);
    Task<IEnumerable<T>> QueryAsync<T>(string sql, object? parameters = null);
}
