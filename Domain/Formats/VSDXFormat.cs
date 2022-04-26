namespace TerrEditor.Domain;

public class VSDXFormat : IFormat
{
    public string Name => ".vsdx";
    public void Read(string filename)
    {
        throw new NotImplementedException();
    }
}