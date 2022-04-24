namespace TerrEditor.Domain;

public class Canvas
{
    public Sizez Size;
    public Scale Scale;
    public OtherScale OtherScale;
    public List<Item> Objects;

    public Canvas(Sizez size, Scale scale)
    {
        Size = size;
        Scale = scale;
        Objects = new List<Item>();
        OtherScale = default(OtherScale);
    }

    public void DeleteItem(Item item, Point position)
    {
        Objects.Remove(item);
        //удалить картинку объекта с холста
    }

    public void AddItem(Item item, Point position)
    {
        Objects.Add(item);
        //добавить картинку объекта на холст
    }
}