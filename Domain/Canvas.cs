using System.Drawing;
using TerrEditor.Domain.Items;

namespace TerrEditor.Domain;

public class Canvas
{
    public Size Size;
    public List<Item> Items;

    public Canvas(Size size)
    {
        Size = size;
        Items = new List<Item>();
    }

    public void DeleteItem(Item item, Point position)
    {
        Items.Remove(item);
        //удалить картинку объекта с холста
    }

    public void AddItem(Item item, Point position)
    {
        Items.Add(item);
        //добавить картинку объекта на холст
    }
}