using System.Drawing;

namespace TerrEditor.Domain.Tools;

public class Pipette : ITool
{
    public string Name => "Pipette";
    private Color _ccolor;
    
    public Item DoAction(Item item)
    {
        throw new NotImplementedException();
    }
}