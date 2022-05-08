namespace TerrEditor.Domain.Formats;

public class VsdxFormat : IFormat
{
    public string Name => ".vsdx";
    public void Read(string filename)
    {
        throw new NotImplementedException();
    }
}