using TerrEditor.Domain;
using TerrEditor.Domain.Items;
using TerrEditor.Domain.Tools;

namespace TerrEditor.Application;

public class WorkService
{
    private WorkingTools _tools;
    private ITool _currentTool;
    private Item _currentItem;
    public WorkSpace WorkSpace;
    public ToolType CurrentToolType;
    
    public WorkService(WorkSpace workSpace)
    {
        _tools = WorkingTools.GetInstance();
        WorkSpace = workSpace;
    }

    public void SetItem(Item item)
    {
        _currentItem = item;
    }
    
    public Item DoAction()
    {
        _currentTool = _tools.GetTool(CurrentToolType);
        return _currentTool.DoAction(_currentItem);
    }
}