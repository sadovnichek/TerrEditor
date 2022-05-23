namespace TerrEditor.Domain;

public class WorkSpace : IWorkSpace
{
    public List<Item> Objects { get; }

    public WorkSpace()
    {
        Objects = new List<Item>();
    }

    public void Add(Item item)
    {
        Objects.Add(item);
    }

    public void Remove(Item item)
    {
        Objects.Remove(item);
    }
}