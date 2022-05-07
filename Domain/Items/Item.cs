using System.Drawing;

namespace TerrEditor.Domain;

public class Item
{
    public Guid Id => Guid.NewGuid();
    public Bitmap Image { get; set; }
    public Point Location { get; set; }
    public Color Color { get; set; }
    public Size Size { get; set; }
    public string Name { get; }
    

    public Item(Bitmap image, Point location, Size size, string name, Color color)
    {
        Image = image;
        Location = location;
        Size = size;
        Name = name;
        Color = color;
    }

    public static Item Clone(Item item)
    {
        return new Item(item.Image, item.Location, item.Size, item.Name, item.Color);
    }
}