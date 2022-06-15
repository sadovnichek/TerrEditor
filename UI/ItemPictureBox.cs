using TerrEditor.Domain;
using Timer = System.Windows.Forms.Timer;

namespace UI;

public sealed class ItemPictureBox : Control
{
    private readonly Timer _refresher;
    public Image Image { get; }

    public Item Item;
    public ItemPictureBox(Item item, Image image, MouseMethods mouseMethods)
    {
        Item = item;
        Name = item.Id.ToString();
        Image = image;
        Width = item.Size.Width;
        Height = item.Size.Height;
        Location = new Point(item.Location.X, item.Location.Y);
        SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        BackColor = Color.Transparent;
        Visible = true;
        MouseDown += mouseMethods.Mouse_Down!;
        MouseUp += mouseMethods.Mouse_Up!;
        MouseMove += mouseMethods.Move_Mouse!;
        Click += mouseMethods.OnPictureBoxClick!;
        _refresher = new Timer();
        _refresher.Tick += TimerOnTick!;
        _refresher.Interval = 10;
        _refresher.Enabled = true;
        _refresher.Start();
    }
    
    protected override CreateParams CreateParams
    {
        get
        {
            CreateParams cp = base.CreateParams;
            cp.ExStyle |= 0x20;
            return cp;
        }
    }

    protected override void OnMove(EventArgs e)
    {
        RecreateHandle();
    }


    protected override void OnPaint(PaintEventArgs e)
    {
        e.Graphics.DrawImage(Image, (Width / 2) - (Image.Width / 2), (Height / 2) - (Image.Height / 2));
    }

    protected override void OnPaintBackground(PaintEventArgs e)
    {
        //Do not paint background
    }

    private void TimerOnTick(object source, EventArgs e)
    {
        RecreateHandle();
        _refresher.Stop();
    }
}