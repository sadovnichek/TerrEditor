using System.Drawing;

namespace TerrEditor.Domain;

public class Pipette : ITool
{
    public string Name => "Pipette";
    private Color _ccolor;
    public void DoAction(Item item)
    {
        _ccolor = item.Color;
    }
}