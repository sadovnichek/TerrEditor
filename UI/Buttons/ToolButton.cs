using TerrEditor.Application;
using TerrEditor.Domain;
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
        throw new NotImplementedException();
    }
}

