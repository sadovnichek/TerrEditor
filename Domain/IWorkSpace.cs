namespace TerrEditor.Domain;

public interface IWorkSpace
{
    void Add(Item item);
    void Remove(Item item);
    List<Item> GetItems();
    void Clear();
    void RotateItem(Item item);
}