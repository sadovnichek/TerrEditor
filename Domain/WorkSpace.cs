using System.Drawing;

namespace TerrEditor.Domain;

public  class WorkSpace
{
    public  Item? CurrentObject;
    public  ITool? CurrentTool;
    public  Canvas? CurrentCanvas;
    public Color color;

    public WorkSpace()
    {
        CurrentCanvas = new Canvas(new Size(800,600));
        CurrentObject = default;
        CurrentTool = default;
    }
    
    //методы
    public void DoAction(Item? item)
    {
        CurrentTool.DoAction(item);
    }

}