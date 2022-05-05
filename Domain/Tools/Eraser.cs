using System.Drawing;

namespace TerrEditor.Domain;

public class Eraser : ITool
{
    public string Name => "Eraser";
    public Canvas? canvasItem;

    public void DoAction(Item item)
    {
        canvasItem.DeleteItem(item,item.Location);
    }
}