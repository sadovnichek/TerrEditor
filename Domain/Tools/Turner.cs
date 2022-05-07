using System.Drawing;

namespace TerrEditor.Domain;

public class Turner:ITool
{
    public string Name { get; }
    public void DoAction(Item item)
    {
        item.Location = new Point(item.Location.Y, -item.Location.X);
    }
}