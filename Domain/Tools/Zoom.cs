using System.Drawing;
using TerrEditor.Domain;
using TerrEditor.Infrastructure;

namespace TerrEditor.Domain.Tools;

public class Zoom : ITool
{
    public string Name => "Zoom";

    private static int delta = 100;

    public Item DoAction(Item item)
    {
        item.Image = item.Image.Resize(new Size(item.Size.Width + delta, item.Size.Height + delta));
        item.Size = new Size(item.Size.Width + delta, item.Size.Height + delta);
        return item;
    }
}