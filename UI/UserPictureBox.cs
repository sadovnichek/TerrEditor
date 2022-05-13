using Timer = System.Windows.Forms.Timer;

namespace UI;

public sealed class UserPictureBox : Control
{
    private readonly Timer _refresher;
    public Image Image { get; set; }

    public UserPictureBox()
    {
        SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        BackColor = Color.Transparent;
        _refresher = new Timer();
        _refresher.Tick += TimerOnTick!;
        _refresher.Interval = 50;
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