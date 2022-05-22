using System.Drawing;
using TerrEditor.Domain.Items;
using TerrEditor.Domain.Tools;

namespace TerrEditor.Domain;

public class WorkSpace : IWorkSpace
{
    private List<Item> _objects;

    public WorkSpace()
    {
        _objects = new List<Item>();
    }

    public void Add(Item item)
    {
        _objects.Add(item);
    }

    public void Remove(Item item)
    {
        _objects.Remove(item);
    }
}