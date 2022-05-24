using UI.MouseEvent;

namespace TerrEditor.Domain;

public class WorkSpace : IWorkSpace
{
    public readonly List<Item> Objects;
    private PanelEventRepository _panelEventRepository;

    public WorkSpace(PanelEventRepository panelEventRepository)
    {
        Objects = new List<Item>();
        _panelEventRepository = panelEventRepository;
    }

    public void Add(Item item)
    {
        Objects.Add(item);
        _panelEventRepository.AddEvent(new PanelEvent(PanelEventType.Add, item));
    }

    public void Remove(Item item)
    {
        Objects.Remove(item);
        _panelEventRepository.AddEvent(new PanelEvent(PanelEventType.Remove, item));
    }

    public void Clear()
    {
        while(Objects.Count > 0)
        {
            var item = Objects.First();
            Objects.Remove(item);
            _panelEventRepository.AddEvent(new PanelEvent(PanelEventType.Remove, item));
        }
    }
}