using TerrEditor.Domain.Items;

namespace TerrEditor.Domain;

public class Category
{
    public string Name { get; }

    public List<Item> items;
}