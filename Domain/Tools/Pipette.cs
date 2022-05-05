using System.Drawing;

namespace TerrEditor.Domain;

public class Pipette : ITool
{
    public string Name => "Pipette";
    private Color color;
    public void DoAction(Item item)
    {
        color = item.Color;
    }
}