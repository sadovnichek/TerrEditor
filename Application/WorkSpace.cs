namespace TerrEditor.Domain;

public class WorkSpace : IWorkSpace
{
    private readonly List<Item> _objects;
    private PanelEventRepository _panelEventRepository;

    public WorkSpace(PanelEventRepository panelEventRepository)
    {
        _objects = new List<Item>();
        _panelEventRepository = panelEventRepository;
    }

    public void Add(Item item)
    {
        _objects.Add(item);
        _panelEventRepository.AddEvent(new PanelEvent(PanelEventType.Add, item));
    }

    public void Remove(Item item)
    {
        _objects.Remove(item);
        _panelEventRepository.AddEvent(new PanelEvent(PanelEventType.Remove, item));
    }
    
    public void Clear()
    {
        while(_objects.Count > 0)
        {
            var item = _objects.First();
            _objects.Remove(item);
            _panelEventRepository.AddEvent(new PanelEvent(PanelEventType.Remove, item));
        }
    }

    public List<Item> GetItems()
    {
        return _objects;
    }
}