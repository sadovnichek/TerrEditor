using TerrEditor.Domain.Items;

namespace TerrEditor.Domain.Tools;

public interface ITool
{
    public string Name { get; }

    Item DoAction(Item item);
}