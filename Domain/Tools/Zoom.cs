namespace TerrEditor.Domain;

public class Zoom : ITool
{
    public string Name => "Zoom";
    private double zoomDelta = 0.2;

    private void ZoomPlus()
    {
        throw new NotImplementedException();
    }

    private void ZoomMinus()
    {
        throw new NotImplementedException();
    }

    public void DoAction(Item item)
    {
        throw new NotImplementedException();
    }
}