using System.Drawing;
using System.Net.Sockets;
using TerrEditor.Domain.Items;

namespace TerrEditor.Application;

public class Items
{
    public Item CurrentItem;

    public Items(Item item)
    {
        CurrentItem = new Item(new Point(1, 1), new Size(1, 1));
    }

    public void Add(Point location,Size size)
    {
        CurrentItem=new Item(location, size);
    }
    
}