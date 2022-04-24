namespace TerrEditor.Domain;

public class Pipette
{
    public string Name = "Pipette";

    public void Choose(Item item)
    {
        CurrentObject = item;
    }
}