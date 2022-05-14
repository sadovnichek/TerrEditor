using TerrEditor.Domain;
using TerrEditor.Domain.Tools;

namespace TerrEditor.Application;

public class WorkingTools //singleton
{
    private static WorkingTools _instance;
    private Dictionary<ToolType, ITool> _tools = new();

    private WorkingTools()
    {
        _tools.Add(ToolType.Eraser, new Eraser());
        _tools.Add(ToolType.Highlighter, new Highlight());
        _tools.Add(ToolType.Pipette, new Pipette());
        _tools.Add(ToolType.Turner, new Turner());
        _tools.Add(ToolType.Zoom ,new Zoom());
    }
    
    public static WorkingTools GetInstance()
    {
        if (_instance == null)
            _instance = new WorkingTools();
        return _instance;
    }

    public ITool GetTool(ToolType toolType)
    {
        return _tools[toolType];
    }
}