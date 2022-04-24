namespace TerrEditor.Domain;

public class Highlight
{
    public string Name = "Highlight";

    public void HighlightAndCopy(Item item)
    {
        WorkSpace.CurrentObject = item;
        //buffer.add(o) ??
    }
}