using TerrEditor.Domain;
using TerrEditor.Domain.Items;
using TerrEditor.Domain.Tools;

namespace TerrEditor.Application;

public class WorkService
{
    private WorkingTools tools;
    private ITool _currentTool;
    private Item _currentItem;
    public ToolType CurrentToolType;
    
    public WorkService(WorkSpace workSpace)
    {
        _currentTool = default;
        tools = WorkingTools.GetInstance();
        _currentItem = default;
    }

    public void SetItem(Item item)
    {
        _currentItem = item;
    }
    
    public Item DoAction()
    {
        _currentTool = tools.GetTool(CurrentToolType);
        return _currentTool.DoAction(_currentItem);
    }
}