using System.Drawing.Drawing2D;
using TerrEditor.Application;
using TerrEditor.Domain.Tools;
using UI.Buttons;
using TerrEditor.Domain;
using TerrEditor.Domain.DataBase;
using TerrEditor.Infrastructure;
using UI.MouseEvent;
using Timer = System.Windows.Forms.Timer;

namespace UI;

public partial class MainForm
{
    public static Panel _panel;
    private WorkService _workService;
    private MouseMethods _mouseMethods;
    private readonly DBReqs _assets = new("assets");
    private readonly DBReqs _tools = new("tools");
    private PanelEventRepository _panelEventRepository;
    private SaveLoadService _saveLoadService;
    
    public MainForm(IWorkService service,
        IMouseMethods mouseMethods, 
        PanelEventRepository panelEventRepository,
        SaveLoadService saveLoadService)
    {
        WindowState = FormWindowState.Maximized;
        _panelEventRepository = panelEventRepository;
        _workService = service as WorkService;
        _mouseMethods = mouseMethods as MouseMethods;
        _saveLoadService = saveLoadService;
        InitializeComponent();
        ConfigurePanel();
        setSize();
        SetLabels();
        ConfigureItemButtons();
        ConfigureToolButtons();
        ConfigureSaveLoadButtons();
    }
    
    private void ConfigurePanel()
    {
        var refresher = new Timer();
        refresher.Tick += UpdatePanel!;
        refresher.Interval = 10;
        refresher.Enabled = true;
        refresher.Start();
        _panel = new Panel();
        _panel.Location = new Point(300, 100);
        _panel.AllowDrop = true;
        _panel.Size = new Size(1200, 600);
        _panel.BackgroundImage = _assets.ParsedDBInfo["land"];
        _panel.DragOver += _mouseMethods.Drag_Over!;
        _panel.DragDrop += _mouseMethods.Drag_Drop!;
        _panel.DragEnter += _mouseMethods.Drag_Enter!;
        _panel.MouseWheel += Clicked;
        Controls.Add(_panel);
    }

    private void Clicked(object? sender, MouseEventArgs e)
    {
        if (e.Delta > 0)
        {
            foreach (var a in _panel.Controls)
            {

                var item = (PictureBox)a;
                item.Size=new Size(item.Width+20, item.Height+20);
            }
            _panel.MaximumSize = new Size(_panel.Width+20, _panel.Height+20);
            _panel.Size = new Size(_panel.Width+20, _panel.Height+20);
        }

        else
        {
            if (_panel.Size.Width <= 100 || _panel.Size.Height <= 100) return;
            _panel.MaximumSize = new Size(_panel.Width-20, _panel.Height-20);
            _panel.Size = new Size(_panel.Width-20, _panel.Height-20);

        }
    }
    
    private void SetLabels()
    {
        var itemsLabel = new Label();
        itemsLabel.Location = new Point(0, 0);
        itemsLabel.Text = @"items";

        var toolsLabel = new Label();
        toolsLabel.Location = new Point(200, 0);
        toolsLabel.Text = @"tools";

        Controls.Add(itemsLabel);
        Controls.Add(toolsLabel);
    }

    private void ConfigureToolButtons()
    {
        Controls.Add(new ToolButton(new Rectangle(200, 20, 50, 50), _tools.ParsedDBInfo["eraser"], ToolType.Eraser,
        _workService));
        Controls.Add(new ToolButton(new Rectangle(250, 20, 50, 50), _tools.ParsedDBInfo["brush"], ToolType.Brush,
            _workService));
        Controls.Add(new ToolButton(new Rectangle(300, 20, 50, 50), _tools.ParsedDBInfo["pipka"], ToolType.Pipette,
            _workService));
        Controls.Add(new ToolButton(new Rectangle(350, 20, 50, 50), _tools.ParsedDBInfo["trans"], ToolType.Turner,
            _workService));
        Controls.Add(new ToolButton(new Rectangle(400, 20, 50, 50), _tools.ParsedDBInfo["zoom"], ToolType.Zoom,
            _workService));
        Controls.Add(new ToolButton(new Rectangle(450, 20, 50, 50), _tools.ParsedDBInfo["scale"], ToolType.None,
            _workService));
    }

    private void ConfigureSaveLoadButtons()
    {
        var saveButton = new Button()
        {
            Location = new Point(1000, 10),
            Size = new Size(100, 50),
            Text = @"Save",
        };
        var loadButton = new Button()
        {
            Location = new Point(1100, 10),
            Size = new Size(100, 50),
            Text = @"Load",
        };
        saveButton.Click += _saveLoadService.Save;
        loadButton.Click += _saveLoadService.Load;
        Controls.Add(saveButton);
        Controls.Add(loadButton);
    }

    private void UpdatePanel(object sender, EventArgs eventArgs)
    {
        if (!_panelEventRepository.IsEmpty)
        {
            var @event = _panelEventRepository.Get();
            switch (@event.Type)
            {
                case PanelEventType.Add:
                {
                    _panel.Controls.Add(new ItemPictureBox(@event.Item, _mouseMethods));
                    break;
                }
                case PanelEventType.Remove:
                {
                    _panel.Controls.RemoveByKey(@event.Item.Id.ToString());
                    break;
                }
            }
        }
    }

    private void ConfigureItemButtons()
    {
        foreach (var pair in _assets.ParsedDBInfo)
        {
            itemsPanel.Controls.Add(new ItemButton(new Rectangle(0, 120, 100, 100), 
                pair.Value.Resize(new Size(75, 75)), pair.Key,
                _mouseMethods.Mouse_Down!, _mouseMethods.Mouse_Up!, _mouseMethods.Move_Mouse!));
        }
    }
    
    //УБРАТЬ!!!!
    private Bitmap RotateImage(Bitmap bmp,Point location) {
        Bitmap rotatedImage = new Bitmap(bmp.Width, bmp.Height);
        rotatedImage.SetResolution(bmp.HorizontalResolution, bmp.VerticalResolution);
        using Graphics g = Graphics.FromImage(rotatedImage);
        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        g.TranslateTransform(location.X,location.Y);
        g.RotateTransform(30);
        g.TranslateTransform(-location.X, - location.Y);
        g.DrawImage(bmp, new Point(0, 0));
        return rotatedImage;
    }
}