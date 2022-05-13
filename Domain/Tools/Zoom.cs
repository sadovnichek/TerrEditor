using System.Drawing;
using TerrEditor.Domain.Items;

namespace TerrEditor.Domain.Tools;

public class Zoom : ITool
{
    public string Name => "Zoom";

    public Item DoAction(Item item)
    {
        item.Size = new Size(item.Size.Width + 100, item.Size.Height + 100);
        return item;
    }
}