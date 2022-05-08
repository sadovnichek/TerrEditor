namespace TerrEditor.Domain.Common;

public interface IRepo<T>
{
    void Save(T obj);
    T Get(Guid id);
    void Update();
    void Delete();
}