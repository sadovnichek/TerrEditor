using System.Drawing;
using TerrEditor.Domain.Items;

namespace TerrEditor.Domain.Tools;

public class Zoom : ITool
{
    public string Name => "Zoom";
    
    private int zoomDelta = 20;
    public bool zoom;
    public WorkSpace space;
    public Zoom(WorkSpace work)
    {
        space = work;
    }
    
    private Size DoZoom(Item item)
    {
        return new Size(item.Size.Width+20,item.Size.Height+20);;
    }

    public Item DoAction(Item item)
    {
        space.CurrentObject = item;
        space.CurrentObject!.Size = DoZoom(item);
        return space.CurrentObject;
    }
}