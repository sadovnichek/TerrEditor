namespace UI;

public partial class MainForm : Form
{
    public MainForm()
    {
        this.BackColor=Color.Coral;
        var wwidth = this.Size.Width;
        InitializeComponent();
        WindowState = FormWindowState.Maximized;
        var layout = GetFlowLayoutPanel();
        var pictureBox = new PictureBox();
        pictureBox.Location = new Point(800, 200);
        pictureBox.Size = new Size(800, 600);
        pictureBox.ImageLocation=@"C:\Users\kondr\Desktop\editor\TerrEditor\UI\land.jpg";
        // var buttonToilet = GetStartButton("toilet",new Point(100,150));
        // var buttonChair = GetStartButton("chair",new Point(100,200));
        // var buttonTable = GetStartButton("table",new Point(100,250));
        // layout.Controls.Add(pictureBox);
        // layout.Controls.Add(buttonToilet);
        // layout.Controls.Add(buttonChair);
        // layout.Controls.Add(buttonTable);
        Controls.Add(GetStartButton("tree",new Point(0,0)));
        Controls.Add(GetStartButton("chair",new Point(0,100)));
        Controls.Add(GetStartButton("table",new Point(0,200)));
        Controls.Add(GetStartButton("toilet",new Point(0,300)));
        Controls.Add(GetStartButton("Brush",new Point(wwidth,0)));
        Controls.Add(GetStartButton("Eraser",new Point(wwidth,100)));
        Controls.Add(GetStartButton("Hilhlight",new Point(wwidth,200)));
        Controls.Add(GetStartButton("Pipette",new Point(wwidth,300)));
        Controls.Add(GetStartButton("Zoom",new Point(wwidth,400)));
        Controls.Add(layout); 
        Controls.Add(pictureBox);
    }

    private FlowLayoutPanel GetFlowLayoutPanel()
    {
        return new FlowLayoutPanel()
        {
            Dock = DockStyle.Top,
            AutoSize = true,
            FlowDirection = FlowDirection.LeftToRight
        };
    }

    private Button GetStartButton(string name,Point location)
    {
        
        var a= new Button
        {
            Size = new Size(150, 100),
            Text = name,
            Location = location,
            BackColor = Color.White
        };
        a.Click += On_Click;
        return a;

    }

    public void On_Click(object sender, EventArgs e)
    {
        var button=(Button)sender;

        Thread.Sleep(400);
        changeText();
        async void changeText() 
        {
            var text = button.Text;
            button.Text = ("You clicked on item,now place it on workspace");
            button.BackColor=Color.Red;
            await Task.Delay(5000);
            button.Text =text;
            button.BackColor=Color.White;
        }
    }
}
