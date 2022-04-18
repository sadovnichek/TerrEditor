namespace TerrEditor.Domain;

public interface IRepo<T>
{
    void Save(T obj);
    T Get(Guid id);
    void Update();
    void Delete();
}