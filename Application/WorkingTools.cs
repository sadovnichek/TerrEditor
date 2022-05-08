using TerrEditor.Domain;

namespace TerrEditor.Application;

public class WorkingTools
{
    public List<ITool> Tools = new();

    
    public WorkingTools(WorkSpace workSpace)
    {
        Tools.Add(new Eraser(workSpace));
        Tools.Add(new Highlight(workSpace));
        Tools.Add(new Pipette(workSpace));
        Tools.Add(new Turner(workSpace));
        Tools.Add(new Zoom(workSpace));
    }
}