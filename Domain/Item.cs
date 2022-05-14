using System.Drawing;

namespace TerrEditor.Domain.Items;

public class Item
{
    public Guid Id => Guid.NewGuid();
    public Point Location { get; set; }
    public Size Size { get; set; }
    
    public Item(Point location, Size size)
    {
        Location = location;
        Size = size;
    }
}