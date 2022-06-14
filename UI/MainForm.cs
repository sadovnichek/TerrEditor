using TerrEditor.Application;
using TerrEditor.Domain.Tools;
using UI.Buttons;
using TerrEditor.Domain;
using TerrEditor.Domain.DataBase;
using TerrEditor.Domain.DBRepo;
using TerrEditor.Infrastructure;
using UI.MouseEvent;
using Timer = System.Windows.Forms.Timer;

namespace UI;

public partial class MainForm
{
    public static Panel _panel;
    private IWorkService _workService;
    private IMouseMethods _mouseMethods;
    private readonly BitmapRepository _assets = new(new("assets"));
    private readonly BitmapRepository _tools = new(new("tools"));
    private readonly BitmapRepository _backs = new(new("background"));
    private PanelEventRepository _panelEventRepository;
    private SaveLoadService _saveLoadService;
    
    public MainForm(IWorkService service,
        IMouseMethods mouseMethods, 
        PanelEventRepository panelEventRepository,
        SaveLoadService saveLoadService)
    {
        WindowState = FormWindowState.Maximized;
        _panelEventRepository = panelEventRepository;
        _workService = service;
        _mouseMethods = mouseMethods;
        _saveLoadService = saveLoadService;
        InitializeComponent();
        _assets.GetImages();
        _tools.GetImages();
        _backs.GetImages();
        ConfigurePanel();
        setSize();
        SetLabels();
        ConfigureItemButtons();
        ConfigureToolButtons();
        ConfigureSaveLoadButtons();
        ConfigureChangeBackgroundButton();
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
        _panel.BackgroundImage = _backs.ParsedDBInfo["backMain"];
        _panel.DragOver += _mouseMethods.Drag_Over!;
        _panel.DragDrop += _mouseMethods.Drag_Drop!;
        _panel.DragEnter += _mouseMethods.Drag_Enter!;
        _panel.MouseWheel += Clicked;
        Controls.Add(_panel);
    }

    private void ConfigureChangeBackgroundButton()
    {
        var changeBackgroundButton = new Button
        {
            Location = new Point(1650, 10),
            Text = @"Change background",
            Size = new Size(130, 70),
            BackColor = Color.White
        };
        changeBackgroundButton.MouseEnter += (_, _) => changeBackgroundButton.BackColor = Color.CornflowerBlue;
        changeBackgroundButton.MouseLeave += (_, _) => changeBackgroundButton.BackColor = Color.White;
        changeBackgroundButton.Click += ChangeBackground!;
        Controls.Add(changeBackgroundButton);
    }
    
    private static int call = 0;

    private void ChangeBackground(object sender, EventArgs e)
    {
        var backgrounds = GetBackgroundImages().ToArray();
        _panel.BackgroundImage = backgrounds.ToArray()[call % backgrounds.Length];
        call++;
    }

    private IEnumerable<Image> GetBackgroundImages()
    {
        yield return _backs.ParsedDBInfo["back1"];
        yield return _backs.ParsedDBInfo["back2"];
        yield return _backs.ParsedDBInfo["back3"];
        yield return _backs.ParsedDBInfo["backMain"];
    }
    
    private void Clicked(object? sender, MouseEventArgs e)
    {
        if (e.Delta > 0)
        {
            foreach (var a in _panel.Controls)
            {
                
                if (a is not ItemPictureBox item) continue;
                item.Size=new Size(item.Width+5, item.Height+5);
                item.Image = item.Image.Resize(item.Size);

            }
            
            _panel.Size = new Size(_panel.Width+20, _panel.Height+20);
        }
        
        else
        {
            foreach (var a in _panel.Controls)
            {
                
                if (a is not ItemPictureBox item) continue;
                if (item.Size.Width <= 20 || item.Size.Height <= 20) break;
                item.Size=new Size(item.Width-5, item.Height-5);
                item.Image = item.Image.Resize(item.Size);

            }
            if (_panel.Size.Width <= 100 || _panel.Size.Height <= 100) return;
            
            _panel.Size = new Size(_panel.Width-20, _panel.Height-20);

        }
    }
    
    private void SetLabels()
    {
        var itemsLabel = new Label();
        itemsLabel.Size = new Size(100, 50);
        itemsLabel.Location = new Point(0, 0);
        itemsLabel.Text = @"items";
        itemsLabel.Font = new Font(FontFamily.GenericSansSerif, 18);

        var toolsLabel = new Label();
        toolsLabel.Size = new Size(100, 50);
        toolsLabel.Location = new Point(300, 0);
        toolsLabel.Font = new Font(FontFamily.GenericSansSerif, 18);
        toolsLabel.Text = @"tools";

        Controls.Add(itemsLabel);
        Controls.Add(toolsLabel);
    }

    private void ConfigureToolButtons()
    {
        Controls.Add(new ToolButton(new Rectangle(300, 50, 50, 50), _tools.ParsedDBInfo["eraser"], ToolType.Eraser,
        _workService));
        Controls.Add(new ToolButton(new Rectangle(350, 50, 50, 50), _tools.ParsedDBInfo["zoom"], ToolType.Zoom,
            _workService));
        Controls.Add(new ToolButton(new Rectangle(400, 50, 50, 50), _tools.ParsedDBInfo["eraser"], ToolType.Flipper,
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
}