using TerrEditor.Domain.Tools;

namespace TerrEditor.Domain;

public interface IWorkService
{
    public void SetItem(Item item);

    public Item DoAction();

    public void SetToolType(ToolType type);
}