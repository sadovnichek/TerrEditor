using System.Drawing;
using TerrEditor.Domain.Items;

namespace TerrEditor.Domain.Tools;

public class Zoom : ITool
{
    public string Name => "Zoom";
    private int zoomDelta = 20;
    public bool zoom;
    public WorkSpace space;
    public Zoom(WorkSpace work)
    {
        space = work;
    }

    private Size ZoomPlus(Size size)
    {
        return new Size(size.Width+zoomDelta,size.Height+zoomDelta);
    }

    private Size ZoomMinus(Size size)
    {
        if (size.Width-zoomDelta<=0||size.Height-zoomDelta<=0)
        {
            Console.WriteLine("Size can not be negative");
            return size;
        }
        return new Size(size.Width-zoomDelta,size.Height-zoomDelta);;
    }

    public void DoAction(Item item)
    {
        space.CurrentObject = item;
        space.CurrentObject!.Size=zoom ? ZoomPlus(item.Size) : ZoomMinus(item.Size);
    }
}