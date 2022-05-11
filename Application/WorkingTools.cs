using TerrEditor.Domain;
using TerrEditor.Domain.Tools;

namespace TerrEditor.Application;

public class WorkingTools
{
    public Dictionary<string,ITool> Tools = new();

    
    public WorkingTools(WorkSpace workSpace)
    {
        Tools.Add("eraser",new Eraser(workSpace));
        Tools.Add("highlighter",new Highlight(workSpace));
        Tools.Add("pipette",new Pipette(workSpace));
        Tools.Add("turner",new Turner(workSpace));
        Tools.Add("zoomer",new Zoom(workSpace));
    }
}