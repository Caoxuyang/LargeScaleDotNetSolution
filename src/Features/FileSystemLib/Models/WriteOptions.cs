using System.Text;

namespace LargeScaleSolution.FileSystemLib.Models;

public sealed class WriteOptions
{
    public bool Overwrite { get; set; } = true;
    public bool CreateDirectory { get; set; } = true;
    public Encoding? Encoding { get; set; }
}
