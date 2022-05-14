using System.Drawing;
using TerrEditor.Domain.Items;

namespace TerrEditor.Domain.Tools;

public class Turner : ITool
{
    public string Name { get; }
    
    public Item DoAction(Item item)
    {
        throw new NotImplementedException();
    }
}