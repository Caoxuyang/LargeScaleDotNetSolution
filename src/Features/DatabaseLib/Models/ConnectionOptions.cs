namespace LargeScaleSolution.DatabaseLib.Models;

public sealed class ConnectionOptions
{
    public string DatabasePath { get; set; } = "app.db";
    public bool CreateIfNotExists { get; set; } = true;

    public string ToConnectionString()
    {
        return $"Data Source={DatabasePath}";
    }
}
