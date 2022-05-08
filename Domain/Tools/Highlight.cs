using TerrEditor.Domain.Items;

namespace TerrEditor.Domain.Tools;

public class Highlight:ITool
{
    public string Name => "Highlight";
    public WorkSpace work;

    public Highlight(WorkSpace space)
    {
        work = space;
    }
    public void DoAction(Item item)
    {
        /*byte r = item.Color.R; 

        var bmp = new Bitmap(item.Image);
        for (int x = 0; x < bmp.Width; x++)
        {
            for (int y = 0; y < bmp.Height; y++)
            {
                Color gotColor = bmp.GetPixel(x, y);
                gotColor = Color.FromArgb(r, gotColor.G, gotColor.B);
                bmp.SetPixel(x, y, gotColor);
            }
        }
        item.Image = bmp;*/
        work.CurrentObject = Item.Clone(item);
    }

    
}