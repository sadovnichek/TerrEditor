using System.Drawing;
using TerrEditor.Domain.Items;

namespace TerrEditor.Application;

public class Items
{
    public List<Item> ListOfItems = new();


    public Items()
    {
        ListOfItems.Add(new Item(new Bitmap(1,2),new Point(1,1),new Size(1,1),"new",new Color()));
        ListOfItems.Add(new Item(new Bitmap(1,3),new Point(1,1),new Size(1,1),"new",new Color()));
        ListOfItems.Add(new Item(new Bitmap(1,4),new Point(1,1),new Size(1,1),"new",new Color()));
        ListOfItems.Add(new Item(new Bitmap(1,5),new Point(1,1),new Size(1,1),"new",new Color()));
        ListOfItems.Add(new Item(new Bitmap(1,6),new Point(1,1),new Size(1,1),"new",new Color()));
    }
}