using TerrEditor.Domain.Tools;

namespace UI.Buttons;
public sealed class ToolButton : UserButton
{
    public ToolButton(Rectangle geometry, Image image, ToolType toolType) : base(geometry, "")
    {
        Image = MainForm.ResizeImage(image, geometry.Size);
        BackColor = Color.White;
        if (toolType == ToolType.Eraser)
            Click += EraserHandler;
    }

    private void EraserHandler(object? sender, EventArgs e)
    {
        BackColor = (BackColor == Color.White) ? Color.CornflowerBlue : Color.White;
        MainForm._service.SetToErase();
    }
}

