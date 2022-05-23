using System.Drawing.Drawing2D;
using TerrEditor.Application;
using TerrEditor.Domain.Tools;
using UI.Buttons;
using MySql.Data.MySqlClient;
using TerrEditor.Domain;
using TerrEditor.Domain.DataBase;
using TerrEditor.Infrastructure;
using UI.MouseEvent;
using Timer = System.Windows.Forms.Timer;

#pragma warning disable CS8618

namespace UI;

public partial class MainForm : Form
{
    public static Panel _panel;
    public static WorkService _service; // почему static почему public?
    private static WorkSpace _workSpace;
    private MouseMethods _mouseMethods;
    private readonly DBReqs _assets = new("assets");
    private readonly DBReqs _tools = new("tools");
    private PanelEventRepository _panelEventRepository;
    private readonly Timer _refresher;
    
    private void ConfigurePanel()
    {
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
        Controls.Add(new ToolButton(new Rectangle(200, 20, 50, 50), _tools.ParsedDBInfo["eraser"], ToolType.Eraser));
        Controls.Add(new ToolButton(new Rectangle(250, 20, 50, 50), _tools.ParsedDBInfo["brush"], ToolType.Brush));
        Controls.Add(new ToolButton(new Rectangle(300, 20, 50, 50), _tools.ParsedDBInfo["pipka"], ToolType.Pipette));
        Controls.Add(new ToolButton(new Rectangle(350, 20, 50, 50), _tools.ParsedDBInfo["trans"], ToolType.Turner));
        Controls.Add(new ToolButton(new Rectangle(400, 20, 50, 50), _tools.ParsedDBInfo["zoom"], ToolType.Zoom));
        Controls.Add(new ToolButton(new Rectangle(450, 20, 50, 50), _tools.ParsedDBInfo["scale"], ToolType.None));
    }

    public MainForm(IWorkService service, 
        IWorkSpace workSpace, 
        IMouseMethods mouseMethods, PanelEventRepository panelEventRepository)
    {
        _panelEventRepository = panelEventRepository;
        Text = @"Landscape Editor";
        WindowState = FormWindowState.Maximized;
        _service = service as WorkService;
        _workSpace = workSpace as WorkSpace;
        _mouseMethods = mouseMethods as MouseMethods;
        BackColor = Color.Azure;
        InitializeComponent();
        ConfigurePanel();
        setSize();
        SetLabels();
        ConfigureItemButtons();
        ConfigureToolButtons();
        
        _refresher = new Timer();
        _refresher.Tick += UpdatePanel!;
        _refresher.Interval = 10;
        _refresher.Enabled = true;
        _refresher.Start();
    }

    public void UpdatePanel(object sender, EventArgs eventArgs)
    {
        if (!_panelEventRepository.IsEmpty)
        {
            _panel.Controls.Clear();
            foreach (var obj in _workSpace.Objects)
            {
                var mouseEvent = _panelEventRepository.Get();
                _panel.Controls.Add(new ItemPictureBox(obj, _mouseMethods));
            }
            _panel.Invalidate();
        }
    }
    
    public static void OnPictureBoxClick(object sender, EventArgs eventArgs)
    {
        if (sender is not ItemPictureBox pictureBox) 
            return;
        if (eventArgs is not MouseEventArgs mouseEventArgs) 
            return;
        _service.SetItem(new Item(pictureBox.Location, pictureBox.Size));
        switch (_service.CurrentToolType)
        {
            case ToolType.Eraser:
            {
                _workSpace.Remove(pictureBox.Item);
                break;
            }
            case ToolType.Zoom:
            {
                if ((mouseEventArgs.Button & MouseButtons.Left) != 0)
                    Zoom.delta = 40;
                else if ((mouseEventArgs.Button & MouseButtons.Right) != 0)
                    Zoom.delta = -40;
                pictureBox.Size = _service.DoAction().Size;
                pictureBox.Image = pictureBox.Image.Resize(pictureBox.Size);
                break;
            }
            case ToolType.Turner:
            {
                /*var image = RotateImage(new Bitmap(pictureBox.Image),_service.DoAction().Location);
                var pb = CreatePictureBox(image);
                _panel.Controls.Remove(pictureBox);
                _panel.Controls.Add(pb);*/
                break;
            }
            case ToolType.Brush:
                break;
            case ToolType.Pipette:
                break;
            case ToolType.Highlighter:
                break;
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