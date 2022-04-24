namespace TerrEditor.Domain;

public class Zoom
{
    public string Name = "Zoom";
    private double zoomDelta = 0.2;

    public void ZoomPlus()
    {
        WorkSpace.CurrentCanvas.OtherScale += zoomDelta;
    }

    public void ZoomMinus()
    {
        WorkSpace.CurrentCanvas.OtherScale -= zoomDelta;
    }
}