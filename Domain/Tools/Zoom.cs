using System.Drawing;
using TerrEditor.Domain.Items;

namespace TerrEditor.Domain.Tools;

public class Zoom : ITool
{
    public string Name => "Zoom";
    
    public static int delta = 100;

    public Item DoAction(Item item)
    {
        if (item.Size.Width + delta <= 0 || item.Size.Height + delta <= 0)
            return item;
        item.Size = new Size(item.Size.Width + delta, item.Size.Height + delta);
        return item;
    }
}