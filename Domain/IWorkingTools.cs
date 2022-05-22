using TerrEditor.Domain.Tools;

namespace TerrEditor.Domain;

public interface IWorkingTools
{
    ITool GetTool(ToolType type);
}