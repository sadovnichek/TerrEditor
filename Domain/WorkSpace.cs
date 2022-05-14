using System.Drawing;
using TerrEditor.Domain.Items;
using TerrEditor.Domain.Tools;

namespace TerrEditor.Domain;

public class WorkSpace // singleton
{
    private List<Item> _objects;
    private static WorkSpace _instance;

    private WorkSpace()
    {
        _objects = new List<Item>();
    }

    public static WorkSpace GetInstance()
    {
        if (_instance is null)
            _instance = new WorkSpace();
        return _instance;
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