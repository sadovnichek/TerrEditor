using TerrEditor.Domain;
using TerrEditor.Domain.Tools;

namespace TerrEditor.Application;

public class WorkService : IWorkService
{
    private readonly IWorkingTools _tools;
    private ITool _currentTool;
    private ToolType CurrentToolType { get; set; }
    
    public WorkService(IWorkingTools tools)
    {
        _tools = tools;
    }
    
    public void SetToolType(ToolType toolType)
    {
        CurrentToolType = toolType;
    }
    
    public Item DoAction(Item item)
    {
        if (CurrentToolType == ToolType.None)
            return item;
        _currentTool = _tools.GetTool(CurrentToolType);
        return _currentTool.DoAction(item);
    }
}