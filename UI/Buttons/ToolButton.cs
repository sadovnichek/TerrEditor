namespace UI.Buttons;

public class ToolButton : UserButton
{
    public ToolButton(Rectangle geometry, Image image) : base(geometry, "")
    {
        Image = MainForm.ResizeImage(image, geometry.Size);
        BackColor = Color.White;
        Click += (_, _) => BackColor = (BackColor == Color.White) ? Color.CornflowerBlue : Color.White;
    }
}