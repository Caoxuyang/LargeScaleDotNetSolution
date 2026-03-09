using LargeScaleSolution.DatabaseLib.Context;
using LargeScaleSolution.DatabaseLib.Models;
using Xunit;

namespace LargeScaleSolution.DatabaseLib.Tests;

public class SqliteDataContextTests : IDisposable
{
    private readonly SqliteDataContext _context;

    public SqliteDataContextTests()
    {
        _context = new SqliteDataContext("Data Source=:memory:");
    }

    [Fact]
    public async Task ExecuteAsync_CreatesTable()
    {
        await _context.ExecuteAsync("CREATE TABLE TestTable (Id INTEGER PRIMARY KEY, Name TEXT)");

        var result = await _context.QueryFirstOrDefaultAsync<int>(
            "SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name='TestTable'");

        Assert.Equal(1, result);
    }

    [Fact]
    public async Task QueryFirstOrDefaultAsync_ReturnsValue()
    {
        await _context.ExecuteAsync("CREATE TABLE Numbers (Value INTEGER)");
        await _context.ExecuteAsync("INSERT INTO Numbers VALUES (42)");

        var result = await _context.QueryFirstOrDefaultAsync<int>("SELECT Value FROM Numbers");

        Assert.Equal(42, result);
    }

    [Fact]
    public async Task QueryAsync_ReturnsMultipleValues()
    {
        await _context.ExecuteAsync("CREATE TABLE Items (Name TEXT)");
        await _context.ExecuteAsync("INSERT INTO Items VALUES ('Item1')");
        await _context.ExecuteAsync("INSERT INTO Items VALUES ('Item2')");

        var results = await _context.QueryAsync<string>("SELECT Name FROM Items");

        Assert.Equal(2, results.Count());
    }

    [Fact]
    public void ConnectionOptions_ToConnectionString_ReturnsValidString()
    {
        var options = new ConnectionOptions { DatabasePath = "test.db" };

        var connectionString = options.ToConnectionString();

        Assert.Equal("Data Source=test.db", connectionString);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
