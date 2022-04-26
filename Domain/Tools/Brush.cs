using System.Drawing;

namespace TerrEditor.Domain;

public class Brush : ITool
{
    public string Name => "Brush";

    public void DoAction(Item item)
    {
        throw new NotImplementedException();
    }
}