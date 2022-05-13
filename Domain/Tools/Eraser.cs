using System.Drawing;
using TerrEditor.Domain.Items;

namespace TerrEditor.Domain.Tools;

public class Eraser : ITool
{
    public string Name => "Eraser";
    public WorkSpace workspace;

    public Eraser(WorkSpace space)
    {
        workspace = space;
    }

    public Item DoAction(Item item)
    {
        workspace.CurrentObject = item;
        workspace.CurrentObject.Size = new Size(0, 0);
        return workspace.CurrentObject;
    }
}