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
        var text = new Label();
        text.Location = new Point(0, 0);
        text.Text = "items";
        var text1 = new Label();
        text1.Location = new Point(wwidth, 0);
        text1.Text = "tools";
        Controls.Add(text);
        Controls.Add(text1);
        Controls.Add(GetStartButton("tree",new Point(0,20)));
        Controls.Add(GetStartButton("chair",new Point(0,120)));
        Controls.Add(GetStartButton("table",new Point(0,220)));
        Controls.Add(GetStartButton("toilet",new Point(0,320)));
        Controls.Add(GetStartButton("Brush",new Point(wwidth,20)));
        Controls.Add(GetStartButton("Eraser",new Point(wwidth,120)));
        Controls.Add(GetStartButton("Hilhlight",new Point(wwidth,220)));
        Controls.Add(GetStartButton("Pipette",new Point(wwidth,320)));
        Controls.Add(GetStartButton("Zoom",new Point(wwidth,420)));
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
