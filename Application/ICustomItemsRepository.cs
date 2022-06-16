using System.Drawing;

namespace TerrEditor.Application;

public interface ICustomItemsRepository
{
    void Add(string name, Image image);

    void Update(string name);
}