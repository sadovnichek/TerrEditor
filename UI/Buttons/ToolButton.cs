using TerrEditor.Application;
using TerrEditor.Domain;
using TerrEditor.Infrastructure;

namespace UI.Buttons;
public sealed class ToolButton : UserButton
{
    private ToolType _currentToolType;
    private ToolHandler _toolHandler;

    public ToolButton(Rectangle geometry, Image image, ToolType toolType, ToolHandler toolHandler) : base(geometry, "")
    {
        Image = image.Resize(geometry.Size);
        BackColor = Color.White;
        _currentToolType = toolType;
        _toolHandler = toolHandler;
        Click += ToolHandler;
    }

    private void ToolHandler(object? sender, EventArgs e)
    {
        _toolHandler.SetToolType(_currentToolType);
    }
}

