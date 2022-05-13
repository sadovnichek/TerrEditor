using System.Drawing;
using System.Windows.Forms;
using TerrEditor.Domain;
using TerrEditor.Domain.Items;
using TerrEditor.Domain.Tools;

namespace TerrEditor.Application;

public class WorkService
{
    private WorkingTools tools;
    public ITool? CurrentTool;
    private Item CurrentItem;
    public ToolType currentType;
    
    public WorkService(WorkSpace workSpace)
    {
        CurrentTool = default;
        tools = new WorkingTools(workSpace);
        CurrentItem = default;
    }

    public void SetToTurn()
    {
       CurrentTool=tools.Tools["turner"];
       currentType = ToolType.Turner;
    }
    public void SetToZoom()
    {
        CurrentTool=tools.Tools["zoomer"];
        currentType = ToolType.Zoom;
    }
    
    public void SetToErase()
    {
        CurrentTool=tools.Tools["eraser"];
        currentType = ToolType.Eraser;
    }
    public void SetToHighlight()
    {
        CurrentTool=tools.Tools["highlighter"];
        currentType = ToolType.Highlighter;
    }
    public void SetToPipette()
    {
        CurrentTool=tools.Tools["pipette"];
        currentType = ToolType.Pipette;
    }

    public void SetItem(Item item)
    {
        CurrentItem = item;
    }

    public Item DoAction()
    {
        return CurrentTool.DoAction(CurrentItem);
    }
}