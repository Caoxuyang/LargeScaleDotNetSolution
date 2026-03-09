namespace LargeScaleSolution.Abstractions;

public interface IFileStorage
{
    Task WriteAsync(string path, string content);
    Task WriteAsync(string path, byte[] data);
    Task<string> ReadAsync(string path);
}
