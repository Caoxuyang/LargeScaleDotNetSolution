using LargeScaleSolution.Abstractions;
using LargeScaleSolution.CommonUtilities;

namespace LargeScaleSolution.FileSystemLib.Services;

public sealed class FileWriter : IFileStorage
{
    public async Task WriteAsync(string path, string content)
    {
        Guard.NotNullOrEmpty(path, nameof(path));
        Guard.NotNull(content, nameof(content));

        var directory = Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        await File.WriteAllTextAsync(path, content);
    }

    public async Task WriteAsync(string path, byte[] data)
    {
        Guard.NotNullOrEmpty(path, nameof(path));
        Guard.NotNull(data, nameof(data));

        var directory = Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        await File.WriteAllBytesAsync(path, data);
    }

    public async Task<string> ReadAsync(string path)
    {
        Guard.NotNullOrEmpty(path, nameof(path));

        if (!File.Exists(path))
            throw new FileNotFoundException("File not found.", path);

        return await File.ReadAllTextAsync(path);
    }
}
