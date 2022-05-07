using System.Drawing;

namespace TerrEditor.Domain;

public static class WorkSpace
{
    public static Canvas Canvases;
    public static Item? CurrentObject;
    public static ITool? CurrentTool;
    public static Canvas? CurrentCanvas;

    static WorkSpace()
    {
        Canvases = new Canvas(new Size(800,600));
        CurrentObject = default;
        CurrentTool = default;
    }
    
    //методы
    public static void DoAction(Item? item)
    {
        CurrentTool.DoAction(item);
    }

}