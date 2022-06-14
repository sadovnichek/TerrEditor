namespace TerrEditor.Domain.Formats;

public interface IFormat
{
    string Name { get; }
    List<Item> Read(string filename);

    void Write(string filename, List<Item> items);
}