﻿using TerrEditor.Domain.Items;

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
        workspace.CurrentCanvas.DeleteItem(item,item.Location);
        return workspace.CurrentObject;
    }
}