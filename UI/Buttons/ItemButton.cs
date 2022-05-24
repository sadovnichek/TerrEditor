namespace UI.Buttons;

public class ItemButton : UserButton
{
    public ItemButton(Rectangle geometry, 
        Image image, string name,
        MouseEventHandler mouseDown,
        MouseEventHandler mouseUp,
        MouseEventHandler moveMouse) : base(geometry, "")
    {
        Image = image;
        Name = name;
        BackColor = Color.White;
        MouseEnter +=(_, _) => BackColor = Color.CornflowerBlue;
        MouseLeave += (_, _) => BackColor = Color.White;
        MouseDown += mouseDown;
        MouseUp += mouseUp;
        MouseMove += moveMouse;
    }

    public sealed override Color BackColor
    {
        get => base.BackColor;
        set => base.BackColor = value;
    }
}