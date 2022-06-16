namespace TerrEditor.Domain;

[Serializable]
public class Item
{
    public Guid Id = Guid.NewGuid();
    public Point2D Location { get; set; }
    public Size Size { get; set; }
    
    public string ImageName { get; private set; }

    public Item(Point2D location, Size size, string imageName)
    {
        Location = location;
        Size = size;
        ImageName = imageName;
    }
}