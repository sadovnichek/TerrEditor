using TerrEditor.Domain.Tools;

namespace TerrEditor.Domain;

public interface IWorkService
{
    public Item DoAction(Item item);

    public void SetToolType(ToolType type);
}