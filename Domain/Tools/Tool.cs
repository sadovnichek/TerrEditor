namespace TerrEditor.Domain;

public abstract class Tool
{
    public string Name { get; }
    
    public Func<Item, Item> DoAction { get; }
}