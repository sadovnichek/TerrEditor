using System.Drawing;
using System.Net.Sockets;
using TerrEditor.Domain.Items;

namespace TerrEditor.Application;

public class Items
{
    public List<Item> ListOfItems = new();

    public void Add(string Name, Point location,Size size)
    {
        ListOfItems.Add(new Item(location, size, Name));
    }
    
}