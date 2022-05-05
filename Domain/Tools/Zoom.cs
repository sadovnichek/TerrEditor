using System.Drawing;
using System.Drawing.Printing;

namespace TerrEditor.Domain;

public class Zoom : ITool
{
    public string Name => "Zoom";
    private int zoomDelta = 20;
    public bool zoom;

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
        item.Size=zoom ? ZoomPlus(item.Size) : ZoomMinus(item.Size);
    }
}