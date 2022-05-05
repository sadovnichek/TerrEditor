using System.Drawing;

namespace TerrEditor.Domain;

public class Item
{
    public Guid Id => Guid.NewGuid();
    public Bitmap Image { get; set; }
    public Point Location { get; }
    public Color Color { get; set; }
    public Size Size { get; set; }
    public string Name { get; }
    

    public Item(Bitmap image, Point location, Size size, string name,Color color)
    {
        this.Image = image;
        this.Location = location;
        this.Size = size;
        this.Name = name;
        Color = color;
    }

    public static Item Clone(Item item)
    {
        return new Item(item.Image, item.Location, item.Size, item.Name, item.Color);
    }
}