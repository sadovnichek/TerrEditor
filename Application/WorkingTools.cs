﻿using TerrEditor.Domain;
using TerrEditor.Domain.Tools;

namespace TerrEditor.Application;

public class WorkingTools : IWorkingTools
{
    private readonly Dictionary<ToolType, ITool> _tools = new();

    public WorkingTools()
    {
        _tools.Add(ToolType.Eraser, Eraser.GetInstance());
        _tools.Add(ToolType.Highlighter, new Highlight());
        _tools.Add(ToolType.Pipette, new Pipette());
        _tools.Add(ToolType.Turner, Turner.GetInstance());
        _tools.Add(ToolType.Zoom ,new Zoom());
    }
    
    public ITool GetTool(ToolType toolType)
    {
        return _tools[toolType];
    }
}