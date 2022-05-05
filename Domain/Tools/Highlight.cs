using System.Drawing;

namespace TerrEditor.Domain;

public class Highlight
{
    public string Name => "Highlight";

    public void HighlightAndCopy(Item item)
    {
        byte r = item.Color.R; 

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
        item.Image = bmp;
        var newItem=Item.Clone(item);
    }
}