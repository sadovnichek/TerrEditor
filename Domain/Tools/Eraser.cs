using System.Drawing;
using UI.MouseEvent;

namespace TerrEditor.Domain.Tools;

public class Eraser : ITool
{
    public string Name => "Eraser";
    private PanelEventRepository _panelEventRepository;

    public Eraser(PanelEventRepository panelEventRepository)
    {
        _panelEventRepository = panelEventRepository;
    }

    public Item DoAction(Item item)
    {
        _panelEventRepository.AddEvent(new PanelEvent(PanelEventType.Remove, item));
        return item;
    }
}