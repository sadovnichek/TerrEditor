using TerrEditor.Domain.Items;

namespace TerrEditor.Domain;

public interface IWorkSpace
{
    void Add(Item item);
    void Remove(Item item);
}