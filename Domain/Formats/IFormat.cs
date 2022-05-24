namespace TerrEditor.Domain.Formats;

public interface IFormat
{
    string Name { get; }
    void Read(string filename);
}