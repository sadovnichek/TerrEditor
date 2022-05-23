using System.Runtime.Serialization.Formatters.Binary;
using TerrEditor.Domain;
using UI.MouseEvent;

namespace UI;

public class SaveService
{
    private PanelEventRepository _panelEventRepository;
    private WorkSpace _workSpace;
    
    public SaveService(PanelEventRepository panelEventRepository, IWorkSpace workSpace)
    {
        _panelEventRepository = panelEventRepository;
        _workSpace = workSpace as WorkSpace;
    }
    
    public void Serialize(object sender, EventArgs eventArgs)
    {
        Stream s = File.Open("data.json", FileMode.Open); 
        BinaryFormatter b = new BinaryFormatter();
        List<Item> data = MainForm._workSpace.Objects.ToList();
        b.Serialize(s, data);
        s.Close();
    }
    
    public void Deserialize(object sender, EventArgs eventArgs)
    {
        Stream s = File.OpenRead("data.json");
        BinaryFormatter b = new BinaryFormatter();
        List<Item> data = (List<Item>) b.Deserialize(s);
        foreach(var a in data)
        {
            _panelEventRepository.AddEvent(new PanelEvent(PanelEventType.Add, a));
            _workSpace.Objects.Add(a);
        }
        s.Close();
    }
}