namespace TerrEditor.Domain;

public interface IWorkService
{
    public void SetItem(Item item);

    public Item DoAction();
}