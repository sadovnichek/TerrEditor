using System.Drawing;

namespace TerrEditor.Infrastructure.DBRepo;

public class BitmapRepository:IImageRepo
{
    private DatabaseCore _core;
    public Dictionary<string, Bitmap> ParsedDBInfo { get; private set; } = new();

    public BitmapRepository(DatabaseCore core)
    {
        _core = core;
    }
    public void GetImages()
    {
        ParsedDBInfo = _core.ParseInfoFromDB();
    }
}