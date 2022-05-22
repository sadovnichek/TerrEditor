using TerrEditor.Domain;
using TerrEditor.Domain.Items;
using TerrEditor.Domain.Tools;

namespace TerrEditor.Application;

public class WorkService : IWorkService
{
    private readonly WorkingTools _tools;
    private ITool _currentTool;
    private Item _currentItem;
    private WorkSpace _workSpace;
    public ToolType CurrentToolType;
    
    public WorkService(IWorkSpace workSpace)
    {
        _tools = WorkingTools.GetInstance();
        _workSpace = workSpace as WorkSpace;
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