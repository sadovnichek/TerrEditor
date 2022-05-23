using TerrEditor.Domain;
using TerrEditor.Domain.Tools;

namespace TerrEditor.Application;

public class WorkService : IWorkService
{
    private readonly WorkingTools _tools;
    private ITool _currentTool;
    private Item _currentItem;
    public ToolType CurrentToolType;
    
    public WorkService(IWorkingTools tools)
    {
        _tools = tools as WorkingTools;
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