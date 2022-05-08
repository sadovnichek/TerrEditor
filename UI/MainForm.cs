using TerrEditor.Application;

namespace UI;
public partial class MainForm : Form
{
    private Panel _panel;
    private Rectangle _dragBoxFromMouseDown;
    private Bitmap _currentSelectedImage;
    private Image _tree;
    private Image _chair;

    private static Image ResizeImage(Image imgToResize, Size size)
    {
        return new Bitmap(imgToResize, size);
    }

    private void SetImages()
    {
        _tree = new Bitmap("./tree.png");
        _tree = ResizeImage(_tree, new Size(100, 100));
        _chair = new Bitmap("./chair.png");
        _chair = ResizeImage(_chair, new Size(100, 100));
        setSize();
    }

    private void ConfigurePanel()
    {
        _panel = new();
        _panel.Location = new Point(800, 200);
        _panel.AllowDrop = true;
        _panel.Size = new Size(800, 600);
        _panel.BackgroundImage = Image.FromFile(Directory.GetCurrentDirectory()+@"\land.jpg");
        _panel.DragOver += Drag_Over!;
        _panel.DragDrop += Drag_Drop!;
        _panel.DragEnter += Drag_Enter!;
        Controls.Add(_panel);
    }

    private void SetLabels(int width)
    {
        var itemsLabel = new Label();
        itemsLabel.Location = new Point(0, 0);
        itemsLabel.Text = "items";
        
        var toolsLabel = new Label();
        toolsLabel.Location = new Point(width, 0);
        toolsLabel.Text = "tools";
        
        Controls.Add(itemsLabel);
        Controls.Add(toolsLabel);
    }
    
    public MainForm(WorkingPlace place)
    {
        BackColor = Color.Coral;
        var width = Size.Width;
        InitializeComponent();
        WindowState = FormWindowState.Maximized;
        ConfigurePanel();
        SetImages();
        SetLabels(width);
        
        
        Controls.Add(GetStartButton("",new Point(0,20), ControlType.Item, "", _tree));
        Controls.Add(GetStartButton("",new Point(0,120), ControlType.Item, "", _chair));
        Controls.Add(GetStartButton("table",new Point(0,220), ControlType.Item));
        Controls.Add(GetStartButton("toilet",new Point(0,320), ControlType.Item));
        Controls.Add(GetStartButton("Brush",new Point(width,20), ControlType.Tool,"щетка"));
        Controls.Add(GetStartButton("Eraser",new Point(width,120), ControlType.Tool,"ластик"));
        Controls.Add(GetStartButton("Highlight",new Point(width,220), ControlType.Tool,"хайлайтер"));
        Controls.Add(GetStartButton("Pipette",new Point(width,320), ControlType.Tool,"пипетка"));
        Controls.Add(GetStartButton("Zoom",new Point(width,420), ControlType.Tool,"изменение размера элемента"));
        Controls.Add(GetStartButton("Выход", new Point(1000, 0), ControlType.Control, "Нажмите, чтобы покинуть программу"));
        Controls.Add(GetStartButton("Фон", new Point(1000, 100), ControlType.Control, "Нажмите, чтобы сменить фоновое изображение"));
    }

    public sealed override Color BackColor
    {
        get => base.BackColor;
        set => base.BackColor = value;
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
        temp.Width = 100;
        temp.Height = 100;
        temp.Image = _currentSelectedImage;
        temp.Location = new Point(Cursor.Position.X - _panel.Location.X - temp.Image.Width / 2, 
            Cursor.Position.Y - _panel.Location.Y - temp.Image.Height / 2);
        temp.BackColor = Color.Transparent;
        temp.Visible = true;
        temp.Click += OnPictureBoxClick!;
        temp.MouseDown += Mouse_Down!;
        temp.MouseUp += Mouse_Up!;
        temp.MouseMove += Move_Mouse!;
        return temp;
    }
    
    private void Drag_Drop(object sender, DragEventArgs e)
    {
        if (e.Effect == DragDropEffects.Move)
        {
            _panel.Controls.Add(CreatePictureBox());
        }
    }

    private void OnPictureBoxClick(object sender, EventArgs e)
    {
        if (sender is PictureBox picureBox)
        {
            picureBox.BackColor = Color.Black;
        }
    }

    private void Drag_Enter(object sender, DragEventArgs e)
    {
        e.Effect = DragDropEffects.None;
    }
    
    private Button GetStartButton(string name,
        Point location,
        ControlType type,
        string ref_inf="элемент декора",
        Image image = null)
    {
        var a= new Button
        {
            Size = new Size(150, 100),
            Text = name,
            Location = location,
            BackColor = Color.White,
            Image = image
        };
        switch (type)
        {
            case ControlType.Item:
                a.Click += On_Click_Item; 
                a.MouseEnter+=(s, e) => a.BackColor = Color.DarkRed;
                a.MouseLeave += (s, e) => a.BackColor = Color.White;
                a.MouseDown += Mouse_Down;
                a.MouseUp += Mouse_Up;
                a.MouseMove += Move_Mouse;
                break;
            case ControlType.Tool:
                a.Click += On_Click_Tool;
                a.MouseEnter+=(s, e) => a.BackColor = Color.CornflowerBlue;
                a.MouseLeave += (s, e) =>a.BackColor = Color.White;
                break;
            case ControlType.Control:
                a.Click += On_Click_Control;
                a.MouseEnter+=(s, e) => a.BackColor = Color.CornflowerBlue;
                a.MouseLeave += (s, e) =>a.BackColor = Color.White;
                break;
        }
        var toolT = new ToolTip();
        toolT.SetToolTip(a,ref_inf);
        return a;

    }
    private static int call = 0;
    private void On_Click_Control(object sender, EventArgs e)
    {
        if (String.Compare(((Button)sender).Text, "Выход", StringComparison.Ordinal) == 0)
        {
            Application.Exit();
        }
        else if (String.Compare(((Button)sender).Text, "Фон", StringComparison.Ordinal) == 0)
        {
            var backgrounds = Background.GetBackgroundImages().ToArray();
            _panel.BackgroundImage = backgrounds.ToArray()[call % backgrounds.Length];
            call++;
        }
    }

    private void On_Click_Item(object sender, EventArgs e)
    {
        var button=(Button)sender;
        Thread.Sleep(400);
        Tool_button_behavior();
        async void Tool_button_behavior() 
        {
            var text = button.Text;
            button.Text = "You clicked on item,now place it on workspace";
            await Task.Delay(5000);
            button.Text =text;
        }
    }

    public void On_Click_Tool(object sender, EventArgs e)
    {
        var button=(Button)sender;
        
        Thread.Sleep(400);
        Item_button_behavior();
        async void Item_button_behavior()
        {
            // var text = button.Text;
            // button.Text = "";
            // await Task.Delay(1000);
            // button.BackColor=Color.White;
            // button.Text
        }
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
public static class Background
{
    public static IEnumerable<Image> GetBackgroundImages()
    {
        yield return Image.FromFile("./land.jpg");
        yield return Image.FromFile("./wooden.jpeg");
        yield return Image.FromFile("./dessert.jpg");
        yield return Image.FromFile("./forest.jpg");
        yield return Image.FromFile("./stones.jpeg");
    }
}
