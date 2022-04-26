using System.Drawing;

namespace TerrEditor.Domain;

public class Eraser : ITool
{
    public string Name => "Eraser";

    public void DoAction(Item item)
    {
        throw new NotImplementedException();
    }
}