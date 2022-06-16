namespace TerrEditor.Domain;

public enum PanelEventType
{
    Add,
    Remove,
    RotateItem
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