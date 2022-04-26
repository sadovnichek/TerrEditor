namespace UI;

public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
        WindowState = FormWindowState.Maximized;
        var layout = GetFlowLayoutPanel();
        layout.Controls.Add(GetStartButton());
        layout.Controls.Add(GetStartButton());
        layout.Controls.Add(GetStartButton());
        layout.Controls.Add(GetStartButton());
        Controls.Add(layout);
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

    private Button GetStartButton()
    {
        return new Button()
        {
            Size = new Size(100, 100)
        };
    }
}