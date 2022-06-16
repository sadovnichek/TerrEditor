using TerrEditor.Application;
using UI.Buttons;
using TerrEditor.Domain;
using TerrEditor.Infrastructure;
using TerrEditor.Infrastructure.DBRepo;
using Size = System.Drawing.Size;
using Timer = System.Windows.Forms.Timer;

namespace UI;

public partial class MainForm
{
    public static Panel _panel;
    private MouseMethods _mouseMethods;
    private readonly BitmapRepository _assets = new(new("assets"));
    private readonly BitmapRepository _tools = new(new("tools"));
    private readonly BitmapRepository _backs = new(new("background"));
    private PanelEventRepository _panelEventRepository;
    private SaveLoadService _saveLoadService;
    private ToolHandler _toolHandler;
    
    public MainForm(MouseMethods mouseMethods, 
        PanelEventRepository panelEventRepository,
        SaveLoadService saveLoadService,
        ToolHandler toolHandler)
    {
        WindowState = FormWindowState.Maximized;
        _panelEventRepository = panelEventRepository;
        _mouseMethods = mouseMethods;
        _saveLoadService = saveLoadService;
        _toolHandler = toolHandler;
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
        Controls.Add(new ToolButton(new Rectangle(300, 50, 50, 50), _tools.ParsedDBInfo["eraser"], ToolType.Eraser, _toolHandler));
        Controls.Add(new ToolButton(new Rectangle(350, 50, 50, 50), _tools.ParsedDBInfo["zoom"], ToolType.Zoom, _toolHandler));
        Controls.Add(new ToolButton(new Rectangle(400, 50, 50, 50), _tools.ParsedDBInfo["trans"], ToolType.Turner, _toolHandler));
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
                    _panel.Controls.Add(new ItemPictureBox(@event.Item, 
                        _assets.ParsedDBInfo[@event.Item.ImageName], 
                        _mouseMethods));
                    break;
                }
                case PanelEventType.Remove:
                {
                    _panel.Controls.RemoveByKey(@event.Item.Id.ToString());
                    break;
                }
                case PanelEventType.RotateItem:
                {
                    var pictureBox = (ItemPictureBox)_panel.Controls.Find(@event.Item.Id.ToString(), false).First();
                    pictureBox.Image.RotateFlip(RotateFlipType.Rotate90FlipX);
                    _panel.Controls.RemoveByKey(@event.Item.Id.ToString());
                    _panel.Controls.Add(pictureBox);
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