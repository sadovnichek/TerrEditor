using System.Drawing;
using TerrEditor.Domain.Items;

namespace TerrEditor.Domain.Tools;

public class Turner:ITool
{
    public string Name { get; }
    public WorkSpace work;

    public Turner(WorkSpace space)
    {
        work = space;
    }
    public Item DoAction(Item item)
    {
        work.CurrentObject = item;
        work.CurrentObject.Location = new Point(item.Location.Y, -item.Location.X);
        return work.CurrentObject;
    }
}