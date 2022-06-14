namespace TerrEditor.Domain.Formats;

public class VsdxFormat : IFormat
{
    public string Name => ".vsdx";
    public List<Item> Read(string filename)
    {
        throw new NotImplementedException();
    }

    public void Write(string filename, List<Item> items)
    {
        throw new NotImplementedException();
    }
}