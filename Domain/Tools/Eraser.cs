using System.Drawing;

namespace TerrEditor.Domain;

public class Eraser : ITool
{
    public string Name => "Eraser";
    public WorkSpace workspace;

    public Eraser(WorkSpace space)
    {
        workspace = space;
    }

    public void DoAction(Item item)
    {
        workspace.CurrentObject = item;
        workspace.CurrentCanvas.DeleteItem(item,item.Location);
    }
}