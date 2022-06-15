using System.Windows.Forms;
using TerrEditor.Domain;
using TerrEditor.Domain.Formats;

namespace TerrEditor.Application;

public class SaveLoadService
{
    private readonly IWorkSpace _workSpace;
    private readonly Dictionary<string, IFormat> _formats;
    
    public SaveLoadService(IWorkSpace workSpace)
    {
        _workSpace = workSpace;
        _formats = new Dictionary<string, IFormat>()
        {
            {".fuf", new FufFormat()}
        };
    }
    
    public void Save(object sender, EventArgs eventArgs)
    {
        var saveFileDialog = new SaveFileDialog();
        saveFileDialog.InitialDirectory = "c:\\";
        saveFileDialog.Filter = "default files (*.fuf)|*.fuf";
        saveFileDialog.FilterIndex = 2;
        saveFileDialog.RestoreDirectory = true;
        if(saveFileDialog.ShowDialog() == DialogResult.OK)
        {
            var selectedFile = saveFileDialog.FileName;
            var extention = Path.GetExtension(selectedFile);
            _formats[extention].Write(selectedFile, _workSpace.GetItems());
        }
    }
    
    public void Load(object sender, EventArgs eventArgs)
    {
        using var openFileDialog = new OpenFileDialog();
        openFileDialog.InitialDirectory = "c:\\";
        openFileDialog.Filter = "default files (*.fuf)|*.fuf";
        openFileDialog.FilterIndex = 2;
        openFileDialog.RestoreDirectory = true;
        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            var selectedFile = openFileDialog.FileName;
            var extention = Path.GetExtension(selectedFile);
            var items = _formats[extention].Read(selectedFile);
            _workSpace.Clear();
            items.ForEach(item => _workSpace.Add(item));
        }
    }
}