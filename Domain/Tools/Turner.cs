using System.Drawing;
using TerrEditor.Domain.Items;

namespace TerrEditor.Domain.Tools;

public class Turner : ITool
{
    public string Name { get; }
    private static Turner _instance;

    private Turner()
    {
        
    }

    public static Turner GetInstance()
    {
        if (_instance is null)
            _instance = new Turner();
        return _instance;
    }
    
    public Item DoAction(Item item)
    {
        return item;
    }
}