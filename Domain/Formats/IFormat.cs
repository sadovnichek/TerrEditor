namespace TerrEditor.Domain;

public interface IFormat
{
    string Name { get; }
    void Read(string filename);
}