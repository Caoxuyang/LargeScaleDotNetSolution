using LargeScaleSolution.FileSystemLib.Services;
using Xunit;

namespace LargeScaleSolution.FileSystemLib.Tests;

public class FileWriterTests : IDisposable
{
    private readonly string _testDir;
    private readonly FileWriter _writer;

    public FileWriterTests()
    {
        _testDir = Path.Combine(Path.GetTempPath(), $"FileWriterTests_{Guid.NewGuid()}");
        Directory.CreateDirectory(_testDir);
        _writer = new FileWriter();
    }

    [Fact]
    public async Task WriteAsync_WritesContentToFile()
    {
        var path = Path.Combine(_testDir, "test.txt");
        var content = "Hello, World!";

        await _writer.WriteAsync(path, content);

        Assert.True(File.Exists(path));
        Assert.Equal(content, await File.ReadAllTextAsync(path));
    }

    [Fact]
    public async Task WriteAsync_CreateDirectoryIfNotExists()
    {
        var path = Path.Combine(_testDir, "subdir", "test.txt");
        var content = "Test content";

        await _writer.WriteAsync(path, content);

        Assert.True(File.Exists(path));
    }

    [Fact]
    public async Task ReadAsync_ReadsContentFromFile()
    {
        var path = Path.Combine(_testDir, "read-test.txt");
        var content = "Read this content";
        await File.WriteAllTextAsync(path, content);

        var result = await _writer.ReadAsync(path);

        Assert.Equal(content, result);
    }

    public void Dispose()
    {
        if (Directory.Exists(_testDir))
            Directory.Delete(_testDir, true);
    }
}
