using TerrEditor.Domain;
using TerrEditor.Domain.Tools;

namespace TerrEditor.Application;

public class WorkService : IWorkService
{
    private readonly IWorkingTools _tools;
    private ITool _currentTool;
    private Item _currentItem;
    private ToolType CurrentToolType { get; set; }
    
    public WorkService(IWorkingTools tools)
    {
        _tools = tools;
    }

    public void SetItem(Item item)
    {
        _currentItem = item;
    }

    public void SetToolType(ToolType toolType)
    {
        CurrentToolType = toolType;
    }
    
    public Item DoAction()
    {
        if (CurrentToolType == ToolType.None)
            return _currentItem;
        _currentTool = _tools.GetTool(CurrentToolType);
        return _currentTool.DoAction(_currentItem);
    }
}