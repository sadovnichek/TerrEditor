namespace TerrEditor.Domain;

public class PanelEventRepository
{
    private List<PanelEvent> events = new();
    public bool IsEmpty => events.Count == 0;

    public void AddEvent(PanelEvent @event)
    {
        events.Add(@event);
    }

    public PanelEvent Get()
    {
        if (IsEmpty) 
            return null;
        var @event = events.Last();
        events.Remove(@event);
        return @event;
    }
}