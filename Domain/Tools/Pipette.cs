using System.Drawing;

namespace TerrEditor.Domain;

public class Pipette : ITool
{
    public string Name => "Pipette";
    private Color _ccolor;
    public WorkSpace work;

    public Pipette(WorkSpace work)
    {
        this.work = work;
    }
    public void DoAction(Item item)
    {
        work.CurrentObject = item;
        work.color=item.Color;
    }
}