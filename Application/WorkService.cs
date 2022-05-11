using System.Drawing;
using TerrEditor.Domain;
using TerrEditor.Domain.Items;
using TerrEditor.Domain.Tools;

namespace TerrEditor.Application;

public class WorkService
{
    private WorkingTools tools;
    public ITool? CurrentTool;
    
    public WorkService(WorkSpace workSpace)
    {
        CurrentTool = default;
        tools = new WorkingTools(workSpace);
    }

    public void SetToTurn()
    {
       CurrentTool=tools.Tools["turner"];
    }
    public Item DoZoom(Item item)
    {
        return tools.Tools["zoomer"].DoAction(item);
    }

    public Item DoTurn(Item item)
    {
        return tools.Tools["turner"].DoAction(item);
    }
    public Item DoErase(Item item)
    {
        return tools.Tools["eraser"].DoAction(item);
    }
    public Item DoHighlight(Item item)
    {
        return tools.Tools["highlighter"].DoAction(item);
    }
    public Item DoPipette(Item item)
    {
        return tools.Tools["pipette"].DoAction(item);
    }
    
}