namespace TerrEditor.Domain;

public interface ITool
{
    public string Name { get; }

    void DoAction(Item item);
}