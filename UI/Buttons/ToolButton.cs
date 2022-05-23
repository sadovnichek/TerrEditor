using TerrEditor.Domain.Tools;
using TerrEditor.Infrastructure;

namespace UI.Buttons;
public sealed class ToolButton : UserButton
{
    private ToolType currentToolType;
    
    public ToolButton(Rectangle geometry, Image image, ToolType toolType) : base(geometry, "")
    {
        Image = image.Resize(geometry.Size);
        BackColor = Color.White;
        currentToolType = toolType;
        Click += ToolHandler;
    }

    private void ToolHandler(object? sender, EventArgs e)
    {
        BackColor = (BackColor == Color.White) ? Color.Fuchsia : Color.White;
        Image = (BackColor == Color.Fuchsia)
            ? Image.Resize(new Size(Image.Width - 15, Image.Height - 15))
            : Image.Resize(new Size(Image.Width + 15, Image.Height + 15));
        MainForm._service.CurrentToolType = (MainForm._service.CurrentToolType != currentToolType)
            ? currentToolType : ToolType.None;
    }
}

