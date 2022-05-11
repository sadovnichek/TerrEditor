using System.Drawing;
using TerrEditor.Domain.Items;

namespace TerrEditor.Domain.Tools;

public class Pipette : ITool
{
    public string Name => "Pipette";
    private Color _ccolor;
    public WorkSpace work;

    public Pipette(WorkSpace work)
    {
        this.work = work;
    }
    public Item DoAction(Item item)
    {
        work.CurrentObject = item;
        work.color=item.Color;
        return work.CurrentObject;
    }
}