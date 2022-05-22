using TerrEditor.Domain;
using TerrEditor.Domain.Tools;

namespace TerrEditor.Application;

public class WorkingTools //singleton
{
    private static WorkingTools _instance = null!;
    private readonly Dictionary<ToolType, ITool> _tools = new();

    private WorkingTools()
    {
        _tools.Add(ToolType.Eraser, Eraser.GetInstance());
        _tools.Add(ToolType.Highlighter, new Highlight());
        _tools.Add(ToolType.Pipette, new Pipette());
        _tools.Add(ToolType.Turner, Turner.GetInstance());
        _tools.Add(ToolType.Zoom ,new Zoom());
    }
    
    public static WorkingTools GetInstance()
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
        if (_instance == null)
            _instance = new WorkingTools();
        return _instance;
    }

    public ITool GetTool(ToolType toolType)
    {
        return _tools[toolType];
    }
}