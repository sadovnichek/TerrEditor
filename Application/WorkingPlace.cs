using TerrEditor.Domain;

namespace TerrEditor.Application;

public class WorkingPlace
{
    public WorkSpace _workSpace;
    public WorkingTools Tools;
    public Items items;

    public WorkingPlace()
    {
        _workSpace = new WorkSpace();
        Tools = new WorkingTools(_workSpace);
        items = new Items();
    }
}