namespace TerrEditor.Domain;

public class Brush
{
    public string Name = "Brush";

    public void Paint(Item item, Point position)
    {
        CurrCanvas.AddItem(item, position);
    }
}