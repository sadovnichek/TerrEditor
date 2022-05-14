using System.Drawing;
using TerrEditor.Domain.Items;

namespace TerrEditor.Domain.Tools;

public class Eraser : ITool // singleton
{
    public string Name => "Eraser";
    private static Eraser _instance;
    private Eraser()
    {
        
    }

    public static Eraser GetInstance()
    {
        if (_instance is null)
            _instance = new Eraser();
        return _instance;
    }
    
    public Item DoAction(Item item)
    {
        throw new NotImplementedException();
    }
}