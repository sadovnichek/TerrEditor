﻿using TerrEditor.Domain;

namespace UI.MouseEvent;

public enum PanelEventType
{
    Add,
    Remove
}

public class PanelEvent
{
    public PanelEventType Type { get; set; }
    public Item Item { get; set; }

    public PanelEvent(PanelEventType type, Item item)
    {
        Type = type;
        Item = item;
    }
}