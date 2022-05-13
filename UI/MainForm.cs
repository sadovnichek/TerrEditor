using TerrEditor.Application;
using TerrEditor.Domain.Items;
using TerrEditor.Domain.Tools;
using UI.Buttons;
#pragma warning disable CS8618

namespace UI;
public partial class MainForm : Form
{
    private Panel _panel;
    private Rectangle _dragBoxFromMouseDown;
    private Bitmap _currentSelectedImage;
    private Image _tree;
    private Image _chair;
    public static WorkService _service;

    public static Image ResizeImage(Image imgToResize, Size size)
    {
        return new Bitmap(imgToResize, size);
    }

    private void SetImages() // загрузка из БД
    {
        _tree = new Bitmap("./tree.png");
        _tree = ResizeImage(_tree, new Size(100, 100));
        _chair = new Bitmap("./chair.png");
        _chair = ResizeImage(_chair, new Size(100, 100));
        setSize();
    }

    private void ConfigurePanel()
    {
        _panel = new Panel();
        _panel.Location = new Point(800, 200);
        _panel.AllowDrop = true;
        _panel.Size = new Size(800, 600);
        _panel.BackgroundImage = Image.FromFile(Directory.GetCurrentDirectory()+@"\land.jpg");
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
        toolsLabel.Location = new Point(150, 0);
        toolsLabel.Text = @"tools";
        
        Controls.Add(itemsLabel);
        Controls.Add(toolsLabel);
    }

    private void ConfigureItemButtons()
    {
        Controls.Add(new ItemButton(new Rectangle(0, 20, 100, 100), _tree, Mouse_Down!, Mouse_Up!, Move_Mouse!));
        Controls.Add(new ItemButton(new Rectangle(0, 120, 100, 100), _chair, Mouse_Down!, Mouse_Up!, Move_Mouse!));
    }

    private void ConfigureToolButtons()
    {
        Controls.Add(new ToolButton(new Rectangle(150, 20, 100, 100), Resources.eraser,ToolType.Eraser));
    }

    private void ConfigureChangeBackgroundButton()
    {
        var changeBackgroundButton = new Button
        {
            Location = new Point(1000, 100),
            Text = @"Change background",
            Size = new Size(200, 100),
            BackColor = Color.White
        };
        changeBackgroundButton.MouseEnter += (_, _) => changeBackgroundButton.BackColor = Color.CornflowerBlue;
        changeBackgroundButton.MouseLeave += (_, _) => changeBackgroundButton.BackColor = Color.White;
        changeBackgroundButton.Click += ChangeBackground!;
        Controls.Add(changeBackgroundButton);
    }
    
    public MainForm(WorkService service)
    {
        WindowState = FormWindowState.Maximized;
        _service = service;
        BackColor = Color.Azure;
        InitializeComponent();
        ConfigurePanel();
        SetImages();
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
            case PictureBox clickedPictureBox:
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
            case PictureBox pictureBox when e.Button == MouseButtons.Left:
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
            ? DragDropEffects.Move : DragDropEffects.None;
    }

    private PictureBox CreatePictureBox()
    {
        var temp = new PictureBox();
        temp.Width = _currentSelectedImage.Width;
        temp.Height = _currentSelectedImage.Height;
        temp.Image = _currentSelectedImage;
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
            _panel.Controls.Add(CreatePictureBox());
        }
    }

    private void OnPictureBoxClick(object? sender, EventArgs eventArgs)
    {
        var pb = (PictureBox)sender;
        _service.SetItem(pb.Location,pb.Size,pb.Name);
        _service.DoAction();
        if (_service.currentType == ToolType.Eraser)
        {
            foreach (var x in _panel.Controls)
            {
                if (x.Equals(pb))
                    _panel.Controls.Remove((PictureBox)x);
            }
        }
    }
    private  Image TurnImage(Image imgToResize)
    {
        var a = new Bitmap(imgToResize);
        a.RotateFlip(RotateFlipType.Rotate90FlipX);
        return new Bitmap(imgToResize);
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
    
    public class DrawingPoints
    {
        private int index = 0;
        private Point[] points;
        public DrawingPoints(int size)
        {
            if (size <= 0)
                throw new ArgumentException("Размер должен быть положительным");
            points = new Point[size];
        }

        public void DrawPoint(int x, int y)
        {
            if (index >= points.Length)
                ResetPoints();
            points[index] = new Point(x, y);
            index++;
        }

        public void ResetPoints()
        {
            index = 0;
        }

        public Point[] GetPoints()
        {
            return points;
        }

        public int GetCountPoints()
        {
            return index;
        }
    }

    private bool isDrawing = false;
    private DrawingPoints drawingPoints = new DrawingPoints(2);
    private Pen pen = new Pen(Color.Black, 3f);
    private Bitmap drawingImage = new Bitmap(100, 100);
    private Graphics graphics = null!;

    private void setSize()
    {
        Rectangle rectangle = Screen.PrimaryScreen.Bounds;
        drawingImage = new Bitmap(rectangle.Width, rectangle.Height);
        graphics = Graphics.FromImage(drawingImage);
    }
    
    private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
    {
        isDrawing = true;
    }

    private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
    {
        isDrawing = false;
        drawingPoints.ResetPoints();
    }

    private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
    {
        if (!isDrawing)
            return;

        drawingPoints.DrawPoint(e.X, e.Y);
        if (drawingPoints.GetCountPoints() >= 2)
        {
            graphics.DrawLines(pen, drawingPoints.GetPoints());
            pictureBox1.Image = drawingImage;
            drawingPoints.DrawPoint(e.X, e.Y);
        }
    }
    
    private void MainForm_Load(object sender, EventArgs e)
    {

    }
}