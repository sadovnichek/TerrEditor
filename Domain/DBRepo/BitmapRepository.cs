using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using TerrEditor.Domain.DataBase;
using TerrEditor.Domain.DBRepo;
using TerrEditor.Domain.Tools;

namespace TerrEditor.Domain;

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