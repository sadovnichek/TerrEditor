using TerrEditor.Domain.Items;

namespace TerrEditor.Domain.Tools;

public class Brush : ITool
{
    public string Name => "Brush";

    public Item DoAction(Item item)
    {
        return item;
    }
}