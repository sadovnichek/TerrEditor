using TerrEditor.Application;
using TerrEditor.Domain;
using TerrEditor.Domain.Tools;
using TerrEditor.Infrastructure;

namespace UI.Buttons;
public sealed class ToolButton : UserButton
{
    private ToolType currentToolType;
    private WorkService _workService;
    
    public ToolButton(Rectangle geometry, Image image, ToolType toolType, IWorkService workService) : base(geometry, "")
    {
        Image = image.Resize(geometry.Size);
        _workService = workService as WorkService;
        BackColor = Color.White;
        currentToolType = toolType;
        Click += ToolHandler;
    }

    private void ToolHandler(object? sender, EventArgs e)
    {
        _workService.CurrentToolType = (_workService.CurrentToolType != currentToolType)
                 ? currentToolType : ToolType.None;
    }
}

