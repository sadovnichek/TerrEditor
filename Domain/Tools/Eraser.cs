using System.Drawing;
using TerrEditor.Domain.Items;

namespace TerrEditor.Domain.Tools;

public class Eraser : ITool
{
    public string Name => "Eraser";

    public Item DoAction(Item item)
    {
        throw new NotImplementedException();
    }
}