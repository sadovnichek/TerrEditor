using System.Drawing;
using TerrEditor.Infrastructure;

namespace TerrEditor.Domain.Tools;

public class Turner : ITool
{
    public string Name => "turner";
    public Item DoAction(Item item)
    {
        item.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
        return item;
    }
}