namespace TerrEditor.Domain;

public class Eraser : Tool
{
    public string Name = "Eraser";

    public void Erase(Item item, Point position)
    {
        CurrCanvas.DeleteItem(item, position);
    }
}