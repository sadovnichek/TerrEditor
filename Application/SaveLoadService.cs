using System.Runtime.Serialization.Formatters.Binary;
using TerrEditor.Domain;

namespace TerrEditor.Application;

public class SaveLoadService
{
    private readonly WorkSpace _workSpace;
    
    public SaveLoadService(IWorkSpace workSpace)
    {
        _workSpace = workSpace as WorkSpace;
    }
    
    public void Save(object sender, EventArgs eventArgs)
    {
        using Stream stream = File.Open("saved.bin", FileMode.Create);
        var binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(stream, _workSpace.Objects);
    }
    
    public void Load(object sender, EventArgs eventArgs)
    {
        using Stream stream = File.Open("saved.bin", FileMode.Open);
        var binaryFormatter = new BinaryFormatter();
        var items = (List<Item>)binaryFormatter.Deserialize(stream);
        _workSpace.Clear();
        items.ForEach(item =>
        {
            _workSpace.Add(item);
        });
    }
}