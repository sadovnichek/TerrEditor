using System.Drawing.Drawing2D;
using TerrEditor.Application;
using TerrEditor.Domain.Items;
using TerrEditor.Domain.Tools;
using UI.Buttons;
using MySql.Data.MySqlClient;
using TerrEditor.Domain;
using TerrEditor.Domain.DataBase;
using TerrEditor.Infrastructure;

#pragma warning disable CS8618

namespace UI;

public partial class MainForm : Form
{
    private Panel _panel;
    private Rectangle _dragBoxFromMouseDown;
    private Bitmap _currentSelectedImage;
    public static WorkService _service;

    private readonly DBReqs _assets = new("assets");
    private readonly DBReqs _tools = new("tools");

    
    private void ConfigurePanel()
    {
        _panel = new Panel();
        _panel.Location = new Point(300, 100);
        _panel.AllowDrop = true;
        _panel.Size = new Size(1200, 600);
        _panel.BackgroundImage = _assets.ParsedDBInfo["land"];
        _panel.DragOver += Drag_Over!;
        _panel.DragDrop += Drag_Drop!;
        _panel.DragEnter += Drag_Enter!;
        Controls.Add(_panel);
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

    private void ConfigureChangeBackgroundButton()
    {
        var changeBackgroundButton = new Button
        {
            Location = new Point(1650, 0),
            Text = @"Change background",
            Size = new Size(130, 70),
            BackColor = Color.White
        };
        changeBackgroundButton.MouseEnter += (_, _) => changeBackgroundButton.BackColor = Color.CornflowerBlue;
        changeBackgroundButton.MouseLeave += (_, _) => changeBackgroundButton.BackColor = Color.White;
        changeBackgroundButton.Click += ChangeBackground!;
        Controls.Add(changeBackgroundButton);
    }

    public MainForm(IWorkService service)
    {
        Text = @"Landscape Editor";
        WindowState = FormWindowState.Maximized;
        _service = service as WorkService;
        BackColor = Color.Azure;
        InitializeComponent();
        ConfigurePanel();
        setSize();
        SetLabels();
        ConfigureItemButtons();
        ConfigureToolButtons();
        ConfigureChangeBackgroundButton();
    }

    private void Mouse_Down(object sender, MouseEventArgs e)
    {
        var dragSize = SystemInformation.DragSize;
        _dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2),
            e.Y - (dragSize.Height / 2)), dragSize);
        switch (sender)
        {
            case Button clickedButton:
            {
                _currentSelectedImage = new Bitmap(clickedButton.Image);
                break;
            }
            case ItemPictureBox clickedPictureBox:
            {
                _currentSelectedImage = new Bitmap(clickedPictureBox.Image);
                break;
            }
            default:
                _dragBoxFromMouseDown = Rectangle.Empty;
                break;
        }
    }

    private void Mouse_Up(object sender, MouseEventArgs e)
    {
        _dragBoxFromMouseDown = Rectangle.Empty;
    }

    private void Move_Mouse(object sender, MouseEventArgs e)
    {
        switch (sender)
        {
            case Button clickedButton when e.Button == MouseButtons.Left:
            {
                if (_dragBoxFromMouseDown != Rectangle.Empty && !_dragBoxFromMouseDown.Contains(e.X, e.Y))
                {
                    clickedButton.DoDragDrop(_currentSelectedImage, DragDropEffects.All);
                }
                break;
            }
            case ItemPictureBox pictureBox when e.Button == MouseButtons.Left:
            {
                if (_dragBoxFromMouseDown != Rectangle.Empty && !_dragBoxFromMouseDown.Contains(e.X, e.Y))
                {
                    pictureBox.DoDragDrop(_currentSelectedImage, DragDropEffects.All);
                    _panel.Controls.Remove(pictureBox);
                }
                break;
            }
        }
    }

    private void Drag_Over(object sender, DragEventArgs e)
    {
        e.Effect = (e.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move
            ? DragDropEffects.Move
            : DragDropEffects.None;
    }

    private ItemPictureBox CreatePictureBox(Image image)
    {
        var temp = new ItemPictureBox();
        temp.Width = image.Width;
        temp.Height = image.Height;
        temp.Image = image;
        temp.Location = new Point(Cursor.Position.X - _panel.Location.X - temp.Image.Width / 2,
            Cursor.Position.Y - _panel.Location.Y - temp.Image.Height / 2);
        temp.BackColor = Color.Transparent;
        temp.Visible = true;
        temp.MouseDown += Mouse_Down!;
        temp.MouseUp += Mouse_Up!;
        temp.MouseMove += Move_Mouse!;
        temp.Click += OnPictureBoxClick;
        return temp;
    }

    private void Drag_Drop(object sender, DragEventArgs e)
    {
        if (e.Effect == DragDropEffects.Move)
        {
            _panel.Controls.Add(CreatePictureBox(_currentSelectedImage));
        }
    }

    private void OnPictureBoxClick(object sender, EventArgs eventArgs)
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
                _panel.Controls.Remove(pictureBox);
                break;
            }
            case ToolType.Zoom:
            {
                pictureBox.Size = _service.DoAction().Size;
                pictureBox.Image = pictureBox.Image.Resize(pictureBox.Size);
                break;
            }
            case ToolType.Turner:
            {
                var image = RotateImage(new Bitmap(pictureBox.Image),_service.DoAction().Location);
                var pb = CreatePictureBox(image);
                _panel.Controls.Remove(pictureBox);
                _panel.Controls.Add(pb);
                
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

    private void Drag_Enter(object sender, DragEventArgs e)
    {
        e.Effect = DragDropEffects.None;
    }

    private static int call = 0;

    private void ChangeBackground(object sender, EventArgs e)
    {
        var backgrounds = GetBackgroundImages().ToArray();
        _panel.BackgroundImage = backgrounds.ToArray()[call % backgrounds.Length];
        call++;
    }

    private static IEnumerable<Image> GetBackgroundImages()
    {
        yield return Image.FromFile("./wooden.jpeg");
        yield return Image.FromFile("./dessert.jpg");
        yield return Image.FromFile("./forest.jpg");
        yield return Image.FromFile("./stones.jpeg");
        yield return Image.FromFile("./land.jpg");
    }
    
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