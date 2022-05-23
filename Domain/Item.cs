using System.Drawing;

namespace TerrEditor.Domain;

public class Item
{
    public Guid Id => new Guid();
    public Point Location { get; set; }
    public Size Size { get; set; }
    public Image Image { get; set; }
    
    public Item(Point location, Size size)
    {
        Location = location;
        Size = size;
    }

    public Item()
    {
        
    }
}