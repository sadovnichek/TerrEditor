using System.Drawing;

namespace TerrEditor.Domain;

public class Turner:ITool
{
    public string Name { get; }
    public WorkSpace work;

    public Turner(WorkSpace space)
    {
        work = space;
    }
    public void DoAction(Item item)
    {
        work.CurrentObject = item;
        work.CurrentObject.Location = new Point(item.Location.Y, -item.Location.X);
    }
}