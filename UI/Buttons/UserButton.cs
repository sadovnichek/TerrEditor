namespace UI.Buttons;

public class UserButton : Button
{
    public UserButton(Rectangle geometry, string text)
    {
        Location = geometry.Location;
        Size = geometry.Size;
        Text = text;
    }

    public sealed override string Text
    {
        get => base.Text;
        set => base.Text = value;
    }
}