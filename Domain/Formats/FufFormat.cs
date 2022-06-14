using System.Runtime.Serialization.Formatters.Binary;

namespace TerrEditor.Domain.Formats;

public class FufFormat : IFormat
{
    public string Name => ".fuf";
    
    public List<Item> Read(string filename)
    {
        using Stream stream = File.Open(filename, FileMode.Open);
        var binaryFormatter = new BinaryFormatter();
        return (List<Item>) binaryFormatter.Deserialize(stream);
    }

    public void Write(string filename, List<Item> items)
    {
        using Stream stream = File.Open(filename, FileMode.Create);
        var binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(stream, items);
    }
}