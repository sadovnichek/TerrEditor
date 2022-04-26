using System.Drawing;

namespace TerrEditor.Domain;

public class Item
{
    public Guid Id => Guid.NewGuid();
    public Bitmap Image { get; }
    public Point Location { get; }
    public Size Size { get; }
    public string Name { get; }

    public Item(Bitmap image, Point location, Size size, string name)
    {
        this.Image = image;
        this.Location = location;
        this.Size = size;
        this.Name = name;
    }
}