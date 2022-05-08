using TerrEditor.Application;

namespace UI;
public partial class MainForm : Form
{
    private Panel panel = new Panel();
    private Rectangle dragBoxFromMouseDown;
    private Bitmap currentSelectedImage;
    private Image tree;
    private Image chair;
    private Point screenOffset;
    //private PictureBox pictureBox = new();

    public static Image resizeImage(Image imgToResize, Size size)
    {
        return new Bitmap(imgToResize, size);
    }

    private void SetImages()
    {
        tree = new Bitmap($@"{Directory.GetCurrentDirectory()}\tree.png");
        tree = resizeImage(tree, new Size(100, 100));
        chair = new Bitmap($@"{Directory.GetCurrentDirectory()}\chair.png");
        chair = resizeImage(chair, new Size(100, 100));
        setSize();
    }
    
    public MainForm(WorkingPlace place)
    {
        BackColor=Color.Coral;
        var wwidth = Size.Width;
        InitializeComponent();
        WindowState = FormWindowState.Maximized;
        panel.Location = new Point(800, 200);
        panel.AllowDrop = true;
        panel.Size = new Size(800, 600);
        
        panel.BackgroundImage = Image.FromFile(Directory.GetCurrentDirectory()+@"\land.jpg");
        panel.DragOver += DragOver;
        panel.DragDrop += DragDrop;
        panel.DragEnter += DragEnter;

        /*pictureBox.Location = new Point(0, 0);
        pictureBox.AllowDrop = true;
        pictureBox.Size = new Size(100, 100);
        pictureBox.ImageLocation=Directory.GetCurrentDirectory()+@"\land.jpg"; */
        SetImages();
        /*pictureBox.DragOver += DragOver;
        pictureBox.DragDrop += DragDrop;
        pictureBox.DragEnter += DragEnter;*/
        
        var text = new Label();
        text.Location = new Point(0, 0);
        text.Text = "items";
        var text1 = new Label();
        text1.Location = new Point(wwidth, 0);
        text1.Text = "tools";
        var flag = false;
        Controls.Add(text);
        Controls.Add(text1);
        Controls.Add(GetStartButton("",new Point(0,20), Type.Item, "", tree));
        Controls.Add(GetStartButton("",new Point(0,120),Type.Item, "", chair));
        Controls.Add(GetStartButton("table",new Point(0,220),Type.Item));
        Controls.Add(GetStartButton("toilet",new Point(0,320),Type.Item));
        Controls.Add(GetStartButton("Brush",new Point(wwidth,20),Type.Tool,"щетка"));
        Controls.Add(GetStartButton("Eraser",new Point(wwidth,120),Type.Tool,"ластик"));
        Controls.Add(GetStartButton("Highlight",new Point(wwidth,220),Type.Tool,"хайлайтер"));
        Controls.Add(GetStartButton("Pipette",new Point(wwidth,320),Type.Tool,"пипетка"));
        Controls.Add(GetStartButton("Zoom",new Point(wwidth,420),Type.Tool,"изменение размера элемента"));
        Controls.Add(GetStartButton("Выход", new Point(1000, 0), Type.Control, "Нажмите, чтобы покинуть программу"));
        Controls.Add(GetStartButton("Фон", new Point(1000, 100), Type.Control, "Нажмите, чтобы сменить фоновое изображение"));
        Controls.Add(panel);
        //Controls.Add(pictureBox);
    }

    private void GetElement(object sender, MouseEventArgs e) // mouse down
    {
        if (sender is Button)
        {
            var clickedButton = sender as Button;
            currentSelectedImage = new Bitmap(clickedButton.Image);
            var dragSize = SystemInformation.DragSize;
            dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2),
                e.Y - (dragSize.Height / 2)), dragSize);
        }
        else
            dragBoxFromMouseDown = Rectangle.Empty;
    }

    private void ThrowElement(object sender, MouseEventArgs e) // mouse up
    {
        dragBoxFromMouseDown = Rectangle.Empty;
    }

    private void MoveMouse(object sender, MouseEventArgs e)
    {
        var clickedButton = sender as Button;
        if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
        {
            // Если курсор вышел за пределы кнопки - начинаем перетаскивание
            if (dragBoxFromMouseDown != Rectangle.Empty &&
                !dragBoxFromMouseDown.Contains(e.X, e.Y))
            {
                // screenOffset служить для определения границ экрана
                screenOffset = SystemInformation.WorkingArea.Location;
                var dropEffect = clickedButton.DoDragDrop(currentSelectedImage, DragDropEffects.All);
            }
        }
    }

    private void DragOver(object sender, DragEventArgs e)
    {
        if ((e.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move)
        {
            e.Effect = DragDropEffects.Move;
        }
        else
        {
            e.Effect = DragDropEffects.None;
        }
    }

    private void DragDrop(object sender, DragEventArgs e)
    {
        if (e.Effect == DragDropEffects.Move)
        {
            PictureBox temp = new PictureBox();
            panel.Controls.Add(temp);
            temp.Parent = panel;
            temp.Width = 100;
            temp.Height = 100;
            temp.Image = currentSelectedImage;
            temp.Location = new Point(Cursor.Position.X - panel.Location.X - temp.Image.Width / 2, 
                Cursor.Position.Y - panel.Location.Y - temp.Image.Height / 2);
            temp.Visible = true;
        }
    }
    
    private void DragLeave(object sender, DragEventArgs e)
    {
        
    }
    
    private void DragEnter(object sender, DragEventArgs e)
    {
        e.Effect = DragDropEffects.None;
    }
    
    public enum Type
    {
        Tool,
        Item,
        Control
    }
    

    private FlowLayoutPanel GetFlowLayoutPanel()
    {
        return new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            AutoSize = true,
            FlowDirection = FlowDirection.LeftToRight
        };
    }

    private Button GetStartButton(string name,
        Point location,
        Type type,
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
            case Type.Item:
                a.Click += On_Click_Item; 
                a.MouseEnter+=(s, e) => a.BackColor = Color.DarkRed;
                a.MouseLeave += (s, e) => a.BackColor = Color.White;
                a.MouseDown += GetElement;
                a.MouseUp += ThrowElement;
                a.MouseMove += MoveMouse;
                break;
            case Type.Tool:
                a.Click += On_Click_Tool;
                a.MouseEnter+=(s, e) => a.BackColor = Color.CornflowerBlue;
                a.MouseLeave += (s, e) =>a.BackColor = Color.White;
                break;
            case Type.Control:
                a.Click += On_Click_Control;
                a.MouseEnter+=(s, e) => a.BackColor = Color.CornflowerBlue;
                a.MouseLeave += (s, e) =>a.BackColor = Color.White;
                break;
        }
        var toolT = new ToolTip();
        toolT.SetToolTip(a,ref_inf);
        return a;

    }

    private void On_Click_Control(object sender, EventArgs e)
    {
        if (String.Compare(((Button)sender).Text, "Выход", StringComparison.Ordinal) == 0)
        {
            Application.Exit();
        }
        else if (String.Compare(((Button)sender).Text, "Фон", StringComparison.Ordinal) == 0)
        { 
            foreach (Control x in Controls)
            {
                if (x is Panel)
                {
                    var rnd = new Random();
                    var hash = ((Panel)x).BackgroundImage.Size.GetHashCode();
                    var i = rnd.Next(1, 6);
                    var dir = Directory.GetParent(Environment.CurrentDirectory)?.Parent;
                    while (hash == Image.FromFile(dir?.Parent?.FullName+Background.dict[i]).Size.GetHashCode())
                    {
                       i=rnd.Next(1, 6);
                    }

                    ((Panel)x).BackgroundImage = Image.FromFile(dir?.Parent?.FullName+Background.dict[i]);
                }
            }
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

        public bool checkDraw()
        {
            return index > 0;
        }
    }

    private bool isDrawing = false;
    private DrawingPoints drawingPoints = new DrawingPoints(3);
    private Pen pen = new Pen(Color.Black, 3f);
    private Bitmap drawingImage = new Bitmap(100, 100);
    private Graphics graphics;

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
        if (drawingPoints.checkDraw())
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
    public static Dictionary<int, string> dict = new()
    {
        {1,@"\land.jpg"},
        {2,@"\dessert.jpg"},
        {3,@"\forest.jpg"},
        {4,@"\wooden.jpeg"},
        {5,@"\stones.jpeg"}
    };
}
