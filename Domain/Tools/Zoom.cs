namespace TerrEditor.Domain;

public class Zoom
{
    public string Name = "Zoom";
    private double zoomDelta = 0.2;

    public void ZoomPlus()
    {
        CurrCanvas.OtherScale += zoomDelta;
    }

    public void ZoomMinus()
    {
        CurrCanvas.OtherScale -= zoomDelta;
    }
}